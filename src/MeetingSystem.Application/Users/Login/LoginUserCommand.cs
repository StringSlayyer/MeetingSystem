using MeetingSystem.Application.Abstractions.Messaging;
using MeetingSystem.Contracts.Users.Register;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MeetingSystem.Application.Users.Login
{
    public record LoginUserCommand(string Email, string Password) : ICommand<TokenResponse>;
}
