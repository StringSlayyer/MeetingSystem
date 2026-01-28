using MeetingSystem.Application.Abstractions.Data;
using MeetingSystem.Application.Abstractions.Messaging;
using MeetingSystem.Domain.Reservations;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Reservations.Cancel
{
    public sealed class CancelReservationCommandHandler(IApplicationDbContext context)
        : ICommandHandler<CancelReservationCommand>
    {
        public async Task<Result> Handle(CancelReservationCommand command, CancellationToken cancellationToken)
        {
            var reservation = await context.Reservations
                .Include(r => r.Resource)
                .ThenInclude(res => res.Company)
                .FirstOrDefaultAsync(r => r.Id == command.ReservationId, cancellationToken);

            if (reservation is null)
            {
                return Result.Failure(Error.NotFound("Reservation.NotFound", "Reservation not found"));
            }


            var isCreator = reservation.UserId == command.UserId;
            var isManager = reservation.Resource.Company.ManagerId == command.UserId;

            if (!isCreator && !isManager)
            {
                return Result.Failure(Error.Failure("Reservation.Unauthorized", "You are not authorized to cancel this reservation."));
            }

            if (reservation.Status == ReservationStatus.Cancelled)
            {
                return Result.Failure(Error.Conflict("Reservation.AlreadyCancelled", "This reservation is already cancelled."));
            }

            if (reservation.TimeSlot.End < DateTime.UtcNow)
            {
                return Result.Failure(Error.Failure("Reservation.Past", "Cannot cancel a reservation that has already ended."));
            }

            reservation.Cancel();

            await context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
