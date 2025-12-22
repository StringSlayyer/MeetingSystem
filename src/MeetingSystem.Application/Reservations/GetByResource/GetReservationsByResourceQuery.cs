using MeetingSystem.Application.Abstractions.Messaging;
using MeetingSystem.Contracts.Reservations;
using MeetingSystem.Contracts.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Reservations.GetByResource
{
    public sealed record GetReservationsByResourceQuery(Guid ResourceId, DateTime? Start, DateTime? End)
        : IQuery<List<ReservationDTO>>;
}
