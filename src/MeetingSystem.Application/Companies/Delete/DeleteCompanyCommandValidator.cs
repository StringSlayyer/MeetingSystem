using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Companies.Delete
{
    public sealed class DeleteCompanyCommandValidator : AbstractValidator<DeleteCompanyCommand>
    {
        public DeleteCompanyCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.CompanyId).NotEmpty();
        }
    }
}
