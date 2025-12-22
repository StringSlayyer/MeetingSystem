using MeetingSystem.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Reservations.Create
{
    public sealed record CreateReservationCommand(
        Guid ResourceId,
        Guid UserId,
        DateTime StartTime,
        DateTime EndTime,
        string? Note,
        List<string>? AttendeeEmails)
        : ICommand<Guid>;
}
