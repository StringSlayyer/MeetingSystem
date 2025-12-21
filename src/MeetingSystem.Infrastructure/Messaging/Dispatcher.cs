using MeetingSystem.Application.Abstractions.Messaging;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MeetingSystem.Infrastructure.Messaging
{
    public sealed class Dispatcher : IDispatcher
    {
        private readonly IServiceProvider _serviceProvider;
        public Dispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task<Result<T>> Query<T>(IQuery<T> query, CancellationToken cancellationToken)
        {
            Type handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(T));
            object? handler = _serviceProvider.GetService(handlerType);
            MethodInfo handleMethod = handlerType.GetMethod("Handle");
            return await (Task<Result<T>>)handleMethod.Invoke(handler, new object[] { query, cancellationToken });
        }

        public Task<Result<T>> Send<T>(ICommand<T> command, CancellationToken cancellationToken)
        {
            Type handlerType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(T));
            object handler = _serviceProvider.GetService(handlerType);
            MethodInfo handleMethod = handlerType.GetMethod("Handle");
            return (Task<Result<T>>)handleMethod.Invoke(handler, new object[] { command, cancellationToken });
        }

        public Task<Result> Send(ICommand command, CancellationToken cancellationToken)
        {
            Type handlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());
            object handler = _serviceProvider.GetService(handlerType);
            MethodInfo handleMethod = handlerType.GetMethod("Handle");
            return (Task<Result>)handleMethod.Invoke(handler, new object[] { command, cancellationToken });
        }
    }
}
