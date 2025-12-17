using MeetingSystem.Domain.Reservations;
using MeetingSystem.Domain.Users;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Domain.Resources
{
    public abstract class Resource : Entity
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; private set; }
        public string Name { get; private set; }

        private readonly List<Reservation> _reservations = new();
        public IReadOnlyCollection<Reservation> Reservations => _reservations.AsReadOnly();
        protected Resource() { }
        public Resource(string name, Guid companyId)
        {
            Name = name;
            CompanyId = companyId;
        }

        public void Reserve(User user, TimeSlot timeSlot)
        {
            if (_reservations.Any(r => r.TimeSlot.OverlapsWith(timeSlot)))
            {
                throw new Exception("Resource is already reserved for this time slot");
            }

            var reservation = new Reservation(this.Id, user.Id, timeSlot);
            _reservations.Add(reservation);

           // AddDomainEvent(new ReservationCreatedDomainEvent(reservation))
        }
    }
}
