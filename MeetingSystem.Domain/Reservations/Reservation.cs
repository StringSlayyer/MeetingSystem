using SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Domain.Reservations
{
    public sealed class Reservation : Entity
    {
        public Guid ResourceId { get; private set; }
        public Guid UserId { get; private set; }
        public TimeSlot TimeSlot { get; private set; }
        public ReservationStatus Status { get; private set; }

        private Reservation() { }

        internal Reservation(Guid resourceId, Guid userId, TimeSlot timeSlot)
        {
            if (resourceId == Guid.Empty) throw new ArgumentException("Resource is required");
            if (userId == Guid.Empty) throw new ArgumentException("User is required");
            if(timeSlot == null) throw new ArgumentNullException(nameof(timeSlot));

            ResourceId = resourceId;
            UserId = userId;
            TimeSlot = timeSlot;

            Status = ReservationStatus.Pending;
        }
    }
}
