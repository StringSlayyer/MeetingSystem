using MeetingSystem.Application.Abstractions.Messaging;
using MeetingSystem.Contracts.Dashboard;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Dashboards.Get
{
    public sealed record GetUserDashboardQuery(Guid UserId) : IQuery<UserDashboardDTO>;
}
