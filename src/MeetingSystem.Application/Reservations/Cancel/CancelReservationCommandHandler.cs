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
                return Result.Failure(ReservationError.NotFound);
            }


            var isCreator = reservation.UserId == command.UserId;
            var isManager = reservation.Resource.Company.ManagerId == command.UserId;

            if (!isCreator && !isManager)
            {
                return Result.Failure(ReservationError.Unauthorized);
            }

            if (reservation.Status == ReservationStatus.Cancelled)
            {
                return Result.Failure(ReservationError.AlreadyCancelled);
            }

            if (reservation.TimeSlot.End < DateTime.UtcNow)
            {
                return Result.Failure(ReservationError.Past);
            }

            reservation.Cancel();

            await context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
