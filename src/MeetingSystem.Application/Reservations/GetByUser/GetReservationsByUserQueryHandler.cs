using MeetingSystem.Application.Abstractions.Data;
using MeetingSystem.Application.Abstractions.Messaging;
using MeetingSystem.Application.Extensions;
using MeetingSystem.Contracts.Reservations;
using MeetingSystem.Domain.Reservations;
using MeetingSystem.Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Reservations.GetByUser
{
    public sealed class GetReservationsByUserQueryHandler(IApplicationDbContext context, IDateTimeProvider dateTimeProvider)
        : IQueryHandler<GetReservationsByUserQuery, List<ReservationDTO>>
    {
        public async Task<Result<List<ReservationDTO>>> Handle(GetReservationsByUserQuery query, CancellationToken cancellationToken)
        {
            bool userExists = await context.Users.AnyAsync(u => u.Id == query.UserId);
            if (!userExists) return Result.Failure<List<ReservationDTO>>(UserErrors.UserNotFound(query.UserId));

            DateTime? filterStart = query.Start ?? dateTimeProvider.UtcNow;
            DateTime? filterEnd = query.End ?? dateTimeProvider.UtcNow.AddMonths(1);

            if (filterStart > filterEnd) return Result.Failure<List<ReservationDTO>>(ReservationError.WrongDates);

            var entities = await context.Reservations
                .AsNoTracking()
                .Include(r => r.Resource)
                .Where(r =>
                    r.UserId == query.UserId &&
                    r.Status != ReservationStatus.Cancelled &&
                    r.TimeSlot.End > filterStart &&
                    r.TimeSlot.Start < filterEnd)
                .OrderBy(r => r.TimeSlot.Start)
                .ToListAsync(cancellationToken);

            var dtos = entities.Select(r => r.ToDTO(r.Resource.Name));

            return dtos.ToList();
        }
    }
}
