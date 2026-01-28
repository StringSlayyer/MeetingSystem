using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Companies.GetById
{
    public sealed class GetCompanyByIdQueryValidator : AbstractValidator<GetCompanyByIdQuery>
    {
        public GetCompanyByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
