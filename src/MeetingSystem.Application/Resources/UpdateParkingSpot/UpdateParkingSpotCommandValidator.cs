using FluentValidation;
using MeetingSystem.Application.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeetingSystem.Application.Resources.UpdateParkingSpot
{
    public sealed class UpdateParkingSpotCommandValidator : AbstractValidator<UpdateParkingSpotCommand>
    {
        public UpdateParkingSpotCommandValidator()
        {
            RuleFor(x => x.ResourceId).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();

            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.PricePerHour).GreaterThanOrEqualTo(0);

            RuleFor(x => x.Image).ValidImage();
        }

    }
}
