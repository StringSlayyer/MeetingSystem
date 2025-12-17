using MeetingSystem.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Test
{
    public sealed record MultiplyByTwoCommand(int number) : ICommand<int>;
}
