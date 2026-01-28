using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Reservations.GetByUser
{
    public sealed class GetReservationsByUserQueryValidator : AbstractValidator<GetReservationsByUserQuery>
    {
        public GetReservationsByUserQueryValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();

            RuleFor(x => x.End)
                .GreaterThanOrEqualTo(x => x.Start)
                .WithMessage("End date must be after Start date.")
                .When(x => x.Start.HasValue && x.End.HasValue);
        }
    }
}
