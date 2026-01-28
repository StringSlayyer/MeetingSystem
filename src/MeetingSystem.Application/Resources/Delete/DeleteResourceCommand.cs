using MeetingSystem.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Resources.Delete
{
    public sealed record DeleteResourceCommand(Guid UserId, Guid ResourceId) : ICommand;
}
