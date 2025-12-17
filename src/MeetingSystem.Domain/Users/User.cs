using System;
using System.Collections.Generic;
using System.Text;
using SharedKernel;

namespace MeetingSystem.Domain.Users
{
    public sealed class User : Entity
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string PasswordHash { get; private set; }

        public User() { }
        public User(string email, string firstName, string lastName, string passwordHash)
        {
            Id = Guid.NewGuid();
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            PasswordHash = passwordHash;
        }
    }
}
