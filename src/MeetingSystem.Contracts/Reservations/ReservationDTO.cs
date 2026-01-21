using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Contracts.Reservations
{
    public sealed record ReservationDTO(
        Guid Id,
        Guid ResourceId,
        string? ResourceName,
        Guid UserId,
        DateTime Start,
        DateTime End,
        string Status,
        string? Note
        );
}
