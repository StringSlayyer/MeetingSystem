using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Domain.Reservations
{
    public class ReservationStatus
    {
        public static readonly ReservationStatus Pending = new(1, "Pending");
        public static readonly ReservationStatus Confirmed = new(2, "Confirmed");
        public static readonly ReservationStatus Cancelled = new(3, "Cancelled");
        public int Id { get; }
        public string Name { get; }
        public ReservationStatus(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public bool CanTransitionTo(ReservationStatus next)
        {
            if (this == Cancelled) return false; 
            if (this == Confirmed && next == Pending) return false;
            return true;
        }

        
        public static IEnumerable<ReservationStatus> List() =>
            new[] { Pending, Confirmed, Cancelled };
    }
}
