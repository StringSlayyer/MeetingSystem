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
        public Guid Id { get; set; }
        public Guid CompanyId { get; private set; }
        public string Name { get; private set; }
        public DateTime LastBookingUpdate { get; private set;  }

        [Timestamp]
        public byte[] RowVersion { get; private set; }

        protected Resource() { }
        public Resource(string name, Guid companyId)
        {
            Name = name;
            CompanyId = companyId;
        }

        public void NotifyReservationMade()
        {
            LastBookingUpdate = DateTime.UtcNow;
        }
        
    }
}
