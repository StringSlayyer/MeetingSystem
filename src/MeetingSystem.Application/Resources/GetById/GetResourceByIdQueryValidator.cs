using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Resources.GetById
{
    public sealed class GetResourceByIdQueryValidator : AbstractValidator<GetResourceByIdQuery>
    {
        public GetResourceByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
