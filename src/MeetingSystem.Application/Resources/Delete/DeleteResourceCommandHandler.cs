using MeetingSystem.Application.Abstractions.Data;
using MeetingSystem.Application.Abstractions.Messaging;
using MeetingSystem.Application.Abstractions.Services;
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
                return Result.Failure(Error.NotFound("Resource.NotFound", "Resource not found"));
            }

            if (resource.Company?.ManagerId != command.UserId)
            {
                return Result.Failure(Error.Failure("Resource.Unauthorized", "You do not own this resource"));
            }

            bool hasFutureReservations = await context.Reservations
                .AnyAsync(r => r.ResourceId == resource.Id && r.TimeSlot.End > DateTime.UtcNow, cancellationToken);

            if (hasFutureReservations)
            {
                return Result.Failure(Error.Conflict(
                    "Resource.HasFutureBookings",
                    "Cannot delete this resource because it has upcoming reservations. Cancel them first."));
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
