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
                reservation.TimeSlot.Start,
                reservation.TimeSlot.End,
                reservation.Status.Name,
                reservation.Note);
        }
    }
}
