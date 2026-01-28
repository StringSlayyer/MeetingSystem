using MeetingSystem.Domain.Companies;
using MeetingSystem.Domain.Reservations;
using MeetingSystem.Domain.Users;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MeetingSystem.Domain.Resources
{
    public abstract class Resource : Entity
    {
        public Guid Id { get; private set; }
        public Guid CompanyId { get; private set; }
        public virtual Company? Company { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal PricePerHour { get; private set; }
        public string? ImageUrl { get; set; }
        public int Capacity { get; private set; }
        public List<string> Features { get; private set; } = new();
        public DateTime LastBookingUpdate { get; private set;  }
        public bool IsDeleted { get; private set; } = false;

        [Timestamp]
        public byte[] RowVersion { get; private set; }

        protected Resource() { }
        public Resource(string name, Guid companyId, string description, decimal pricePerHour, string? imageUrl, int capacity)
        {
            Id = Guid.NewGuid();
            Name = name;
            CompanyId = companyId;
            Description = description;
            PricePerHour = pricePerHour;
            ImageUrl = imageUrl;
            Capacity = capacity;
        }

        public void AddFeature(string feature)
        {
            if (!Features.Contains(feature)) Features.Add(feature);
        }

        public void ClearFeatures()
        {
            Features.Clear();
        }

        protected void UpdateBaseDetails(string name, string description, decimal pricePerHour, string? imageUrl)
        {
            Name = name;
            Description = description;
            PricePerHour = pricePerHour;

            if (!string.IsNullOrEmpty(imageUrl))
            {
                ImageUrl = imageUrl;
            }
        }

        protected void UpdateCapacity(int capacity)
        {
            Capacity = capacity;
        }
     

        public void NotifyReservationMade()
        {
            LastBookingUpdate = DateTime.UtcNow;
        }

        public void SoftDelete()
        {
            IsDeleted = true;
        }
        
    }
}
