using SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Domain.Reservations
{
    public static class ReservationError
    {
        public static Error TimeOverlaps => Error.Conflict(
            "Reservation.TimeOverlaps", "Time of this reservation is overlapping with another reservation");

        public static Error WrongDates => Error.Conflict(
            "Reservation.WrongDates", "Start time cant be later than end time");
    }
}
