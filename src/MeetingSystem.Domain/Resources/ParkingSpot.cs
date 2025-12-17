using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Domain.Resources
{
    public sealed class ParkingSpot : Resource
    {
        public bool IsCovered { get; private set; }
        public ParkingSpot() { }
        public ParkingSpot(bool isCovered)
        {
            this.IsCovered = isCovered;
        }
    }
}
