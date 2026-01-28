using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Reservations.Create
{
    public sealed class CreateReservationCommandValidator : AbstractValidator<CreateReservationCommand>
    {
        public CreateReservationCommandValidator()
        {
            RuleFor(x => x.ResourceId).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();

            RuleFor(x => x.EndTime)
                .GreaterThan(x => x.StartTime)
                .WithMessage("End time must be after the start time.");

            RuleFor(x => x.StartTime)
                .GreaterThan(DateTime.UtcNow)
                .WithMessage("Meeting cannot start in the past.");

            RuleFor(x => x.Note)
                .MaximumLength(500);

            RuleForEach(x => x.AttendeeEmails)
                .EmailAddress()
                .WithMessage("One or more attendee emails are invalid.")
                .When(x => x.AttendeeEmails != null);
        }
    }
}
