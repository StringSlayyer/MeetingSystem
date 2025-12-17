using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Domain.Companies
{
    public sealed class Address
    {
        public string Number { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
