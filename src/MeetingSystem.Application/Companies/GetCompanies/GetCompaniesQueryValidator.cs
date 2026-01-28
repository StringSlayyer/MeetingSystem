using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Companies.GetCompanies
{
    public sealed class GetCompaniesQueryValidator : AbstractValidator<GetCompaniesQuery>
    {
        public GetCompaniesQueryValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThanOrEqualTo(1);

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(100); 

            RuleFor(x => x.SearchTerm)
                .MaximumLength(100).WithMessage("Search term is too long.")
                .When(x => !string.IsNullOrEmpty(x.SearchTerm));
        }
    }
}
