using FluentValidation;
using MeetingSystem.Application.Abstractions.Messaging;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Abstractions.Behaviors
{
    public static class ValidationDecorator
    {
        public sealed class CommandHandler<TCommand>(
            ICommandHandler<TCommand> innerHandler,
            IEnumerable<IValidator<TCommand>> validators)
            : ICommandHandler<TCommand>
            where TCommand : ICommand
        {
            public async Task<Result> Handle(TCommand command, CancellationToken cancellationToken)
            {
                if (!validators.Any())
                {
                    return await innerHandler.Handle(command, cancellationToken);
                }

                var context = new ValidationContext<TCommand>(command);

                var failures = validators
                    .Select(v => v.Validate(context))
                    .SelectMany(result => result.Errors)
                    .Where(f => f != null)
                    .ToList();

                if (failures.Count != 0)
                {
                    var errorMsg = string.Join("; ", failures.Select(x => x.ErrorMessage).Distinct());

                    var validationError = Error.Validation("Validation.Error", errorMsg);

                    return Result.Failure(validationError);
                }

                return await innerHandler.Handle(command, cancellationToken);
            }
        }

        public sealed class CommandHandler<TCommand, TResponse>(
            ICommandHandler<TCommand, TResponse> innerHandler,
            IEnumerable<IValidator<TCommand>> validators)
            : ICommandHandler<TCommand, TResponse>
            where TCommand : ICommand<TResponse>
        {
            public async Task<Result<TResponse>> Handle(TCommand command, CancellationToken cancellationToken)
            {
                if (!validators.Any())
                {
                    return await innerHandler.Handle(command, cancellationToken);
                }

                var context = new ValidationContext<TCommand>(command);

                var failures = validators
                    .Select(v => v.Validate(context))
                    .SelectMany(result => result.Errors)
                    .Where(f => f != null)
                    .ToList();

                if (failures.Count != 0)
                {
                    var errorMsg = string.Join("; ", failures.Select(x => x.ErrorMessage).Distinct());
                    var validationError = Error.Validation("Validation.Error", errorMsg);

                    
                    return Result<TResponse>.ValidationFailure(validationError);
                }

                return await innerHandler.Handle(command, cancellationToken);
            }
        }

        
        public sealed class QueryHandler<TQuery, TResponse>(
            IQueryHandler<TQuery, TResponse> innerHandler,
            IEnumerable<IValidator<TQuery>> validators)
            : IQueryHandler<TQuery, TResponse>
            where TQuery : IQuery<TResponse>
        {
            public async Task<Result<TResponse>> Handle(TQuery query, CancellationToken cancellationToken)
            {
                if (!validators.Any())
                {
                    return await innerHandler.Handle(query, cancellationToken);
                }

                var context = new ValidationContext<TQuery>(query);

                var failures = validators
                    .Select(v => v.Validate(context))
                    .SelectMany(result => result.Errors)
                    .Where(f => f != null)
                    .ToList();

                if (failures.Count != 0)
                {
                    var errorMsg = string.Join("; ", failures.Select(x => x.ErrorMessage).Distinct());
                    var validationError = Error.Validation("Validation.Error", errorMsg);

                    return Result<TResponse>.ValidationFailure(validationError);
                }

                return await innerHandler.Handle(query, cancellationToken);
            }
        }
    }
}
