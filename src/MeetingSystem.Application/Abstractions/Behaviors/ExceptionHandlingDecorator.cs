using MeetingSystem.Application.Abstractions.Messaging;
using Microsoft.Extensions.Logging;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Abstractions.Behaviors
{
    internal static class ExceptionHandlingDecorator
    {
        internal sealed class CommandHandler<TCommand>(
            ICommandHandler<TCommand> innerHandler,
            ILogger<CommandHandler<TCommand>> logger)
            : ICommandHandler<TCommand>
            where TCommand : ICommand
        {
            public async Task<Result> Handle(TCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    return await innerHandler.Handle(command, cancellationToken);
                }
                catch (DomainException ex)
                {
                    logger.LogWarning(ex, "Domain exception in command {Command}: {Message}", typeof(TCommand).Name, ex.Message);
                    return Result.Failure(Error.Failure(ex.Code, ex.Message));
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Unhandled exception in command {Command}", typeof(TCommand).Name);
                    return Result.Failure(Error.Failure("Server.Error", "An unexpected error occurred."));
                }
            }
        }

        internal sealed class CommandHandler<TCommand, TResponse>(
            ICommandHandler<TCommand, TResponse> innerHandler,
            ILogger<CommandHandler<TCommand, TResponse>> logger)
            : ICommandHandler<TCommand, TResponse>
            where TCommand : ICommand<TResponse>
        {
            public async Task<Result<TResponse>> Handle(TCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    return await innerHandler.Handle(command, cancellationToken);
                }
                catch (DomainException ex)
                {
                    logger.LogWarning(ex, "Domain exception in command {Command}: {Message}", typeof(TCommand).Name, ex.Message);

                    return Result.Failure<TResponse>(Error.Failure(ex.Code, ex.Message));
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Unhandled exception in command {Command}", typeof(TCommand).Name);
                    return Result.Failure<TResponse>(Error.Failure("Server.Error", "An unexpected error occurred."));
                }
            }
        }

        internal sealed class QueryHandler<TQuery, TResponse>(
            IQueryHandler<TQuery, TResponse> innerHandler,
            ILogger<QueryHandler<TQuery, TResponse>> logger)
            : IQueryHandler<TQuery, TResponse>
            where TQuery : IQuery<TResponse>
        {
            public async Task<Result<TResponse>> Handle(TQuery query, CancellationToken cancellationToken)
            {
                try
                {
                    return await innerHandler.Handle(query, cancellationToken);
                }
                catch (DomainException ex)
                {
                    logger.LogWarning(ex, "Domain exception in query {Query}: {Message}", typeof(TQuery).Name, ex.Message);
                    return Result.Failure<TResponse>(Error.Failure(ex.Code, ex.Message));
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Unhandled exception in query {Query}", typeof(TQuery).Name);
                    return Result.Failure<TResponse>(Error.Failure("Server.Error", "An unexpected error occurred."));
                }
            }
        }
    }
}
