using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Domain.Companies
{
    public sealed class Address
    {
        public string Number { get; private set; }
        public string Street { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }

        private Address() { }

        public Address(string street, string number, string city, string state)
        {
            Number = number;
            Street = street;
            City = city;
            State = state;
        }

    }
}
