using System;
using System.Collections.Generic;
using System.Text;
using SharedKernel;

namespace MeetingSystem.Domain.Users
{
    public sealed class User : Entity
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordHash { get; set; }
    }
}
