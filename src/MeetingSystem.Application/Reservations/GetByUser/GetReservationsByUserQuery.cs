using MeetingSystem.Application.Abstractions.Messaging;
using MeetingSystem.Contracts.Reservations;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Reservations.GetByUser
{
    public sealed record GetReservationsByUserQuery(Guid UserId, DateTime? Start, DateTime? End) : IQuery<List<ReservationDTO>>;
}
