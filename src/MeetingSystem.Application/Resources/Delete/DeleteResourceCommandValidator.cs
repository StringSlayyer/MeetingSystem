using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Resources.Delete
{
    public sealed class DeleteResourceCommandValidator : AbstractValidator<DeleteResourceCommand>
    {
        public DeleteResourceCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.ResourceId).NotEmpty();
        }
    }
}
