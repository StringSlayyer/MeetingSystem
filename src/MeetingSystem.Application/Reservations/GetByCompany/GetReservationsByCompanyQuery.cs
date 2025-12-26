using MeetingSystem.Application.Abstractions.Messaging;
using MeetingSystem.Contracts.Reservations;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Reservations.GetByCompany
{
    public sealed record GetReservationsByCompanyQuery(Guid CompanyId, DateTime? Start, DateTime? End)
        : IQuery<List<ReservationDTO>>;
}
