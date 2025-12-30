using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Domain.Resources
{
    public sealed class MeetingRoom : Resource
    {
        public MeetingRoom() { }
        public MeetingRoom(string name, Guid companyId, string description, decimal pricePerHour, string? imageUrl, int capacity)
            : base(name, companyId, description, pricePerHour, imageUrl, capacity)
        {
        }
    }
}
