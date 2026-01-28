using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Resources.GetByCompany
{
    public sealed class GetResourcesByCompanyQueryValidator : AbstractValidator<GetResourcesByCompanyQuery>
    {
        public GetResourcesByCompanyQueryValidator()
        {
            RuleFor(x => x.CompanyId).NotEmpty();
        }
    }
}
