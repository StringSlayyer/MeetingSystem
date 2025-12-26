using MeetingSystem.Application.Abstractions.Data;
using MeetingSystem.Application.Abstractions.Messaging;
using MeetingSystem.Application.Extensions;
using MeetingSystem.Contracts.Reservations;
using MeetingSystem.Domain.Companies;
using MeetingSystem.Domain.Reservations;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Reservations.GetByCompany
{
    public sealed class GetReservationsByCompanyQueryHandler(IApplicationDbContext context)
        : IQueryHandler<GetReservationsByCompanyQuery, List<ReservationDTO>>
    {
        public async Task<Result<List<ReservationDTO>>> Handle(GetReservationsByCompanyQuery query, CancellationToken cancellationToken)
        {
            bool companyExists = await context.Companies.AnyAsync(r => r.Id == query.CompanyId, cancellationToken);

            if (!companyExists) return Result.Failure<List<ReservationDTO>>(CompanyError.CompanyNotFound(query.CompanyId));

            DateTime? filterStart = query.Start ?? DateTime.UtcNow;
            DateTime? filterEnd = query.End ?? DateTime.UtcNow.AddMonths(1);

            if (filterStart > filterEnd) return Result.Failure<List<ReservationDTO>>(ReservationError.WrongDates);

            // 3. The Efficient Query
            var entities = await context.Reservations
                .AsNoTracking()
                .Include(r => r.Resource)
                .Where(r =>
                    r.Resource.CompanyId == query.CompanyId &&
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
