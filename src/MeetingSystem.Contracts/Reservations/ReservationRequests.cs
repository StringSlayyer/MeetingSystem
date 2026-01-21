using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Contracts.Reservations
{
    public sealed record CreateReservationRequest(
      Guid ResourceId,
      DateTime StartTime,
      DateTime EndTime,
      string? Note,
      List<string>? AttendeeEmails);
    public sealed record GetReservationByResourceRequest(Guid ResourceId, DateTime? Start, DateTime? End);

}
