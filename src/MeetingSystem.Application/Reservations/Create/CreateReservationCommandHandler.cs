using MeetingSystem.Application.Abstractions.Data;
using MeetingSystem.Application.Abstractions.Messaging;
using MeetingSystem.Domain.Reservations;
using MeetingSystem.Domain.Resources;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Reservations.Create
{
    public sealed class CreateReservationCommandHandler(IApplicationDbContext context)
        : ICommandHandler<CreateReservationCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateReservationCommand command, CancellationToken cancellationToken)
        {
            var resource = await context.Resources
                .Where(r => r.Id == command.ResourceId).FirstOrDefaultAsync(cancellationToken);

            if (resource is null)
                return Result.Failure<Guid>(ResourceError.NotFound(command.ResourceId));

            var newStart = command.StartTime;
            var newEnd = command.EndTime;

            bool isOverlapping = await context.Reservations
                .AnyAsync(r =>
                r.ResourceId == command.ResourceId &&
                r.Status != ReservationStatus.Cancelled &&
                r.TimeSlot.Start < newEnd && newStart < r.TimeSlot.End, cancellationToken);

            if (isOverlapping) return Result.Failure<Guid>(ReservationError.TimeOverlaps);

            var timeSlot = new TimeSlot(command.StartTime, command.EndTime);

            var reservation = new Reservation(resource.Id, command.UserId, timeSlot, command.Note);

            reservation.Confirm();

            resource.NotifyReservationMade();

            context.Reservations.Add(reservation);

            try
            {
                await context.SaveChangesAsync(cancellationToken);
            }
            catch(DbUpdateConcurrencyException ex)
            {
                return Result.Failure<Guid>(Error.Conflict("DbConcurrecnyConflict", ex.ToString()));
            }

            return reservation.Id;
        }
    }
}
