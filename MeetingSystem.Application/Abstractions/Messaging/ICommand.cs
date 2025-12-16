using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Abstractions.Messaging
{
    public interface ICommand;

    public interface ICommand<TResponse>;
}
