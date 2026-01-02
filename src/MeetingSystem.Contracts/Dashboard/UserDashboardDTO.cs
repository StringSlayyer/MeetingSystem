using MeetingSystem.Contracts.Reservations;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Contracts.Dashboard
{
    public sealed class UserDashboardDTO
    {
        public string Name { get; set; }
        public int ReservationCount { get; set; }
        public List<ReservationDTO> Reservations { get; set; } = new();
    }
}
