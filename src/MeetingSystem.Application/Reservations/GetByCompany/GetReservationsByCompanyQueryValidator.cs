using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Reservations.GetByCompany
{
    public sealed class GetReservationsByCompanyQueryValidator : AbstractValidator<GetReservationsByCompanyQuery>
    {
        public GetReservationsByCompanyQueryValidator()
        {
            RuleFor(x => x.CompanyId).NotEmpty();

            RuleFor(x => x.End)
                .GreaterThanOrEqualTo(x => x.Start)
                .WithMessage("End date must be after Start date.")
                .When(x => x.Start.HasValue && x.End.HasValue);
        }
    }
}
