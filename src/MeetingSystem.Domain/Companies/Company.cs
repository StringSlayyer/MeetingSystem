using MeetingSystem.Domain.Resources;
using MeetingSystem.Domain.Users;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Domain.Companies
{
    public sealed class Company : Entity
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string? ImageUrl { get; set; }

        public Guid ManagerId { get; private set; }
        public User Manager { get; private set; }
        public Address Address { get; private set; }

        public double Rating { get; private set; }
        public int BookingCount { get; private set; }

        private readonly List<Resource> _rooms = new();
        public IReadOnlyCollection<Resource> Rooms => _rooms.AsReadOnly();

        private Company() { }
        public Company(Guid managerId, string name, string description, string? imageUrl, Address address)
        {
            Id = Guid.NewGuid();
            ManagerId = managerId;
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
            Address = address;
            Rating = 5.0;
            BookingCount = 0;
        }

        public void AddRoom(Resource resource)
        {
            _rooms.Add(resource);
        }


    }
}
