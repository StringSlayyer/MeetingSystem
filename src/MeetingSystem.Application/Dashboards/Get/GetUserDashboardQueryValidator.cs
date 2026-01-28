using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Dashboards.Get
{
    public sealed class GetUserDashboardQueryValidator : AbstractValidator<GetUserDashboardQuery>
    {
        public GetUserDashboardQueryValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
