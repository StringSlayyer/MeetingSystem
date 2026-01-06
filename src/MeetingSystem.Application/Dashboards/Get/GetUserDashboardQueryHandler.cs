using MeetingSystem.Application.Abstractions.Data;
using MeetingSystem.Application.Abstractions.Messaging;
using MeetingSystem.Application.Extensions;
using MeetingSystem.Contracts.Dashboard;
using MeetingSystem.Contracts.Reservations;
using MeetingSystem.Domain.Reservations;
using MeetingSystem.Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Dashboards.Get
{
    public sealed class GetUserDashboardQueryHandler(IApplicationDbContext context, IDateTimeProvider dateTimeProvider)
        : IQueryHandler<GetUserDashboardQuery, UserDashboardDTO>
    {
        public async Task<Result<UserDashboardDTO>> Handle(GetUserDashboardQuery query, CancellationToken cancellationToken)
        {
            var user = await context.Users.Where(u => u.Id == query.UserId).FirstOrDefaultAsync(cancellationToken);
            if (user == null) return Result.Failure<UserDashboardDTO>(UserErrors.UserNotFound(query.UserId));

            DateTime start = dateTimeProvider.UtcNow;
            DateTime end = start.AddDays(7);

            var entities = await context.Reservations
                .AsNoTracking()
                .Include(r => r.Resource)
                .Where(r =>
                    r.UserId == query.UserId &&
                    r.Status != ReservationStatus.Cancelled &&
                    r.TimeSlot.End > start &&
                    r.TimeSlot.Start < end)
                .OrderBy(r => r.TimeSlot.Start)
                .ToListAsync(cancellationToken);

            UserDashboardDTO result = new UserDashboardDTO
            {
                Name = user.FirstName,
                ReservationCount = entities.Count,
                Reservations = entities.Take(5).Select(r => r.ToDTO(r.Resource.Name)).ToList()
            };
            return result;
        }
    }
}
