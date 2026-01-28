using FluentValidation;
using MeetingSystem.Application.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeetingSystem.Application.Resources.AddMeetingRoom
{
    public sealed class AddMeetingRoomCommandValidator : AbstractValidator<AddMeetingRoomCommand>
    {
        public AddMeetingRoomCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.CompanyId).NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Meeting room name is required.")
                .MaximumLength(100);

            RuleFor(x => x.Description).MaximumLength(1000);

            RuleFor(x => x.PricePerHour)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Price per hour cannot be negative.");

            RuleFor(x => x.Capacity)
                .GreaterThan(0)
                .WithMessage("Capacity must be at least 1 person.");

            RuleForEach(x => x.Features)
                .NotEmpty()
                .MaximumLength(50)
                .When(x => x.Features != null);

            RuleFor(x => x.Image).ValidImage();
        }

       
    }
}
