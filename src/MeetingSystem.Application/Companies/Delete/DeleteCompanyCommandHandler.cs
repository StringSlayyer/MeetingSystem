using MeetingSystem.Application.Abstractions.Data;
using MeetingSystem.Application.Abstractions.Messaging;
using MeetingSystem.Application.Abstractions.Services;
using MeetingSystem.Domain.Reservations;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Companies.Delete
{
    public sealed class DeleteCompanyCommandHandler(
        IApplicationDbContext context, IFileStorageService fileStorageService)
        : ICommandHandler<DeleteCompanyCommand>
    {
        public async Task<Result> Handle(DeleteCompanyCommand command, CancellationToken cancellationToken)
        {
            var company = await context.Companies
                .Include(c => c.Rooms) 
                .FirstOrDefaultAsync(c => c.Id == command.CompanyId, cancellationToken);

            
            if (company is null)
            {
                return Result.Failure(Error.NotFound("Company.NotFound", "Company not found"));
            }

            if (company.ManagerId != command.UserId)
            {
                return Result.Failure(Error.Failure("Company.Unauthorized", "You are not the manager of this company"));
            }

          
            bool hasFutureReservations = await context.Reservations
                .AnyAsync(r =>
                    r.Resource.CompanyId == company.Id &&
                    r.TimeSlot.End > DateTime.UtcNow &&
                    r.Status != ReservationStatus.Cancelled,
                    cancellationToken);

            if (hasFutureReservations)
            {
                return Result.Failure(Error.Conflict(
                    "Company.HasActiveBookings",
                    "Cannot delete company. There are upcoming reservations for your resources."));
            }

          
            bool hasHistory = await context.Reservations
                .AnyAsync(r => r.Resource.CompanyId == company.Id, cancellationToken);

            if (hasHistory)
            {
                company.SoftDelete();

                foreach (var resource in company.Rooms)
                {
                    resource.SoftDelete();
                }

            }
            else
            {
                foreach (var resource in company.Rooms)
                {
                    if (!string.IsNullOrEmpty(resource.ImageUrl))
                    {
                        await fileStorageService.DeleteFileAsync(resource.ImageUrl);
                    }
                }

               
                context.Companies.Remove(company);
            }

            await context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
