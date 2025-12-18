using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Domain.Resources
{
    public sealed class ParkingSpot : Resource
    {
        public bool IsCovered { get; private set; }
        public ParkingSpot() { }
        public ParkingSpot(string name, Guid companyId, bool isCovered)
            : base(name, companyId)
        {
            this.IsCovered = isCovered;
        }
    }
}
