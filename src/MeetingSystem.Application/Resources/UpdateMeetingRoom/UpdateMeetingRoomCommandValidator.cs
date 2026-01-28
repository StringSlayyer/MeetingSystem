using FluentValidation;
using MeetingSystem.Application.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeetingSystem.Application.Resources.UpdateMeetingRoom
{
    public sealed class UpdateMeetingRoomCommandValidator : AbstractValidator<UpdateMeetingRoomCommand>
    {
        public UpdateMeetingRoomCommandValidator()
        {
            RuleFor(x => x.ResourceId).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();

            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Description).MaximumLength(1000);

            RuleFor(x => x.PricePerHour).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Capacity).GreaterThan(0);

            RuleForEach(x => x.Features)
                .NotEmpty()
                .MaximumLength(50)
                .When(x => x.Features != null);

            RuleFor(x => x.Image).ValidImage();
        }

       
    }
}
