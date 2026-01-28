using SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Domain.Reservations.Events
{
    public sealed record ReservationCreatedDomainEvent(Guid ReservationId, Guid ResourceId) : IDomainEvent;
}
