using SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Abstractions.Messaging
{
    public interface IDispatcher
    {
        Task<Result<T>> Send<T>(ICommand<T> command, CancellationToken cancellationToken);
        Task<Result> Send(ICommand command, CancellationToken cancellationToken);
        Task<Result<T>> Query<T>(IQuery<T> query, CancellationToken cancellationToken);
    }
}
