using MeetingSystem.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Reservations.Cancel
{
    public sealed record CancelReservationCommand(Guid UserId, Guid ReservationId) : ICommand;
}
