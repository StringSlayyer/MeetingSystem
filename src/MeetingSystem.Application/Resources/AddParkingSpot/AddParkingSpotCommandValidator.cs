using FluentValidation;
using MeetingSystem.Application.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeetingSystem.Application.Resources.AddParkingSpot
{
    public sealed class AddParkingSpotCommandValidator : AbstractValidator<AddParkingSpotCommand>
    {
        public AddParkingSpotCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.CompanyId).NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Parking spot name/identifier is required.")
                .MaximumLength(50);

            RuleFor(x => x.PricePerHour)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Capacity)
                .GreaterThan(0)
                .WithMessage("Capacity must be at least 1.");

            RuleFor(x => x.Image).ValidImage();
        }
       
       
    }
}
