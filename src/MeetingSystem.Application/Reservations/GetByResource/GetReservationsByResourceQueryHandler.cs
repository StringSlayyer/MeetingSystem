using MeetingSystem.Application.Abstractions.Data;
using MeetingSystem.Application.Abstractions.Messaging;
using MeetingSystem.Application.Extensions;
using MeetingSystem.Contracts.Reservations;
using MeetingSystem.Domain.Reservations;
using MeetingSystem.Domain.Resources;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Reservations.GetByResource
{
    public sealed class GetReservationsByResourceQueryHandler(IApplicationDbContext context, IDateTimeProvider dateTimeProvider)
        : IQueryHandler<GetReservationsByResourceQuery, List<ReservationDTO>>
    {
        public async Task<Result<List<ReservationDTO>>> Handle(GetReservationsByResourceQuery query, CancellationToken cancellationToken)
        {
            var resourceExists = await context.Resources.AnyAsync(r => r.Id == query.ResourceId, cancellationToken);
            if (!resourceExists)
                return Result.Failure<List<ReservationDTO>>(ResourceError.NotFound(query.ResourceId));

            DateTime? filterStart = query.Start ?? dateTimeProvider.UtcNow;
            DateTime? filterEnd = query.End ?? dateTimeProvider.UtcNow.AddMonths(1);

            if (filterStart > filterEnd) return Result.Failure<List<ReservationDTO>>(ReservationError.WrongDates);

            var entities = await context.Reservations
                .AsNoTracking()
                .Where(r =>
                    r.ResourceId == query.ResourceId &&
                    r.Status != ReservationStatus.Cancelled &&
                    r.TimeSlot.End > filterStart &&
                    r.TimeSlot.Start < filterEnd)
                .OrderBy(r => r.TimeSlot.Start)
                .ToListAsync(cancellationToken);

            if (entities.Count == 0) return new List<ReservationDTO>();
            var dtos = entities.Select(r => r.ToDTO(null));

            return dtos.ToList();
        }
    }
}
