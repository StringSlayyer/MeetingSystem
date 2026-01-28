using MeetingSystem.Contracts.Reservations;
using MeetingSystem.Domain.Reservations;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Extensions
{
    public static class ReservationMappingExtensions
    {
        public static ReservationDTO ToDTO(this Reservation reservation, string? resourceName)
        {
            return new ReservationDTO(
                reservation.Id,
                reservation.ResourceId,
                resourceName,
                reservation.UserId,
                DateTime.SpecifyKind(reservation.TimeSlot.Start, DateTimeKind.Utc),
                DateTime.SpecifyKind(reservation.TimeSlot.End, DateTimeKind.Utc),
                reservation.Status.Name,
                reservation.Note);
        }
    }
}
