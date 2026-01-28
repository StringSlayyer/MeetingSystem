using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Companies.GetByUser
{
    public sealed class GetCompaniesByUserQueryValidator : AbstractValidator<GetCompaniesByUserQuery>
    {
        public GetCompaniesByUserQueryValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();

            RuleFor(x => x.Page)
                .GreaterThanOrEqualTo(1).WithMessage("Page must be at least 1.");

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(100).WithMessage("Page size must not exceed 100.");
        }
    }
}
