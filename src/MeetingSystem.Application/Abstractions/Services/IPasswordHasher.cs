using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Abstractions.Services
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }
}
