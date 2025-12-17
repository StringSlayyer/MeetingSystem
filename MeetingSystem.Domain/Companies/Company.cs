using MeetingSystem.Domain.MeetingRooms;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Domain.Companies
{
    public sealed class Company : Entity
    {
        public string Name { get; set; }
        public Address Address { get; set; }
        public ICollection<MeetingRoom> Rooms { get; set; } = new List<MeetingRoom>();
    }
}
