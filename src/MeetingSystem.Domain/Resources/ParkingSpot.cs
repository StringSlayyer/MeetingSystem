using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Domain.Resources
{
    public sealed class ParkingSpot : Resource
    {
        public bool IsCovered { get; private set; }
        public ParkingSpot() { }
        public ParkingSpot(string name, Guid companyId, string description, decimal pricePerHour, string? imageUrl, bool isCovered)
            : base(name, companyId, description, pricePerHour, imageUrl, 1)
        {
            this.IsCovered = isCovered;
            this.AddFeature(isCovered ? "Covered Parking" : "Open Parking");
        }
    }
}
