using MeetingSystem.Domain.Resources;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Domain.Companies
{
    public sealed class Company : Entity
    {
        public Guid ManagerId { get; private set; }
        public string Name { get; private set; }
        public Address Address { get; private set; }
        public ICollection<Resource> Rooms { get; private set; }

        public Company() { }
        public Company(Guid managerId, string name, Address address)
        {
            ManagerId = managerId;
            Name = name;
            Address = address;
            Rooms = new List<Resource>();
        }


    }
}
