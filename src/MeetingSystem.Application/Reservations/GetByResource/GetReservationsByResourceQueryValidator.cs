using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Reservations.GetByResource
{
    public sealed class GetReservationsByResourceQueryValidator : AbstractValidator<GetReservationsByResourceQuery>
    {
        public GetReservationsByResourceQueryValidator()
        {
            RuleFor(x => x.ResourceId).NotEmpty();

            RuleFor(x => x.End)
                .GreaterThanOrEqualTo(x => x.Start)
                .WithMessage("End date must be after Start date.")
                .When(x => x.Start.HasValue && x.End.HasValue);
        }
    }
}
