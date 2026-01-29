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

        public static Error NotFound => Error.NotFound(
            "Reservation.NotFound", "Reservation not found");

        public static Error Unauthorized => Error.Failure(
            "Reservation.Unauthorized", "You are not authorized to cancel this reservation.");

        public static Error AlreadyCancelled => Error.Conflict(
            "Reservation.AlreadyCancelled", "This reservation is already cancelled.");

        public static Error Past => Error.Failure(
            "Reservation.Past", "Cannot cancel a reservation that has already ended.");
    }
}
