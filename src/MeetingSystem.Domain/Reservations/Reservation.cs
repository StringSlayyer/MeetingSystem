using MeetingSystem.Domain.Resources;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Domain.Reservations
{
    public sealed class Reservation : Entity
    {
        public Guid Id { get; private set; }
        public Guid ResourceId { get; private set; }
        public Resource Resource { get; private set; } = null!;
        public Guid UserId { get; private set; }
        public TimeSlot TimeSlot { get; private set; }
        public ReservationStatus Status { get; private set; }
        public string? Note { get; private set; }

        private Reservation() { }

        public Reservation(Guid resourceId, Guid userId, TimeSlot timeSlot, string? note)
        {
            if (resourceId == Guid.Empty) throw new ArgumentException("Resource is required");
            if (userId == Guid.Empty) throw new ArgumentException("User is required");
            if(timeSlot == null) throw new ArgumentNullException(nameof(timeSlot));

            Id = Guid.NewGuid();
            ResourceId = resourceId;
            UserId = userId;
            TimeSlot = timeSlot;
            Note = note;
            Status = ReservationStatus.Pending;
        }

        public void Confirm()
        {
            if (!Status.CanTransitionTo(ReservationStatus.Confirmed))
            {
                throw new Exception("Cannot confirm this reservation.");
            }

            Status = ReservationStatus.Confirmed;
        }
    }
}
