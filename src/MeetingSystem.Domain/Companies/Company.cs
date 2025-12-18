using MeetingSystem.Domain.Resources;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Domain.Companies
{
    public sealed class Company : Entity
    {
        public Guid Id { get; private set; }
        public Guid ManagerId { get; private set; }
        public string Name { get; private set; }
        public Address Address { get; private set; }

        private readonly List<Resource> _rooms = new();
        public IReadOnlyCollection<Resource> Rooms => _rooms.AsReadOnly();

        public Company() { }
        public Company(Guid managerId, string name, Address address)
        {
            Id = Guid.NewGuid();
            ManagerId = managerId;
            Name = name;
            Address = address;
        }

        public void AddRoom(Resource resource)
        {
            _rooms.Add(resource);
        }


    }
}
