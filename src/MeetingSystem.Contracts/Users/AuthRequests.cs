using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Contracts.Users
{
    public sealed class AuthRequests
    {
        public sealed record RegisterUserRequest(string Email, string FirstName, string LastName, string Password);
        public sealed record LoginUserRequest(string Email, string Password);
    }
}
