using MeetingSystem.Application.Abstractions.Messaging;
using MeetingSystem.Contracts.Users.Register;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Users.Register
{
    public sealed record RegisterUserCommand(string Email, string FirstName, string LastName, string Password) : ICommand<TokenResponse>;
}
