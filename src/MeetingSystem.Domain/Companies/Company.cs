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
        public bool IsDeleted { get; private set; } = false;
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
            bool exists = _rooms.Any(r => r.Name.Equals(resource.Name, StringComparison.InvariantCultureIgnoreCase));

            if (exists)
            {
                throw new DomainException("Company.DuplicateRoom", $"A room with the name '{resource.Name}' already exists in this company.");
            }

            if (_rooms.Count >= 20)
            {
                throw new DomainException("Company.MaxRoomsReached", "This company cannot have more than 20 rooms.");
            }

            if (resource.CompanyId != this.Id)
            {
                throw new DomainException("Company.InvalidResource", "Cannot add a resource linked to a different company.");
            }

            _rooms.Add(resource);
        }

        public void UpdateDetails(string name, string description, Address address, string? imageUrl)
        {
            Name = name;
            Description = description;
            Address = address;

            if(imageUrl is not null)
            {
                ImageUrl = imageUrl;
            }
        }

        public void SoftDelete()
        {
            IsDeleted = true;
        }

        public void IncrementBookingCount()
        {
            BookingCount++;
        }
    }
}
