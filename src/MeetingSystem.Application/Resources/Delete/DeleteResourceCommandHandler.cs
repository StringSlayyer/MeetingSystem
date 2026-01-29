using MeetingSystem.Application.Abstractions.Data;
using MeetingSystem.Application.Abstractions.Messaging;
using MeetingSystem.Application.Abstractions.Services;
using MeetingSystem.Domain.Reservations;
using MeetingSystem.Domain.Resources;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Resources.Delete
{
    public sealed class DeleteResourceCommandHandler(IApplicationDbContext context, IFileStorageService fileStorageService)
        : ICommandHandler<DeleteResourceCommand>
    {
        public async Task<Result> Handle(DeleteResourceCommand command, CancellationToken cancellationToken)
        {
            var resource = await context.Resources
                .Include(r => r.Company)
                .FirstOrDefaultAsync(r => r.Id == command.ResourceId, cancellationToken);

            if (resource is null)
            {
                return Result.Failure(ResourceError.NotFound(command.ResourceId));
            }

            if (resource.Company?.ManagerId != command.UserId)
            {
                return Result.Failure(ResourceError.Unauthorized);
            }

            bool hasFutureReservations = await context.Reservations
                .AnyAsync(r => r.ResourceId == resource.Id && r.TimeSlot.End > DateTime.UtcNow &&
                r.Status != ReservationStatus.Cancelled, cancellationToken);

            if (hasFutureReservations)
            {
                return Result.Failure(ResourceError.HasFutureBookings);
            }

            
            bool hasHistory = await context.Reservations
                .AnyAsync(r => r.ResourceId == resource.Id, cancellationToken);

            if (hasHistory)
            {
                resource.SoftDelete();
            }
            else
            {
                if (!string.IsNullOrEmpty(resource.ImageUrl))
                {
                    await fileStorageService.DeleteFileAsync(resource.ImageUrl);
                }

                context.Resources.Remove(resource);
            }

            await context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
