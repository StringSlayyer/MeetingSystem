using MeetingSystem.Application.Abstractions.Behaviors;
using MeetingSystem.Application.Abstractions.Messaging;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.Scan(scan => scan.FromAssembliesOf(typeof(DependencyInjection))
                .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)), publicOnly: false)
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)), publicOnly: false)
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)), publicOnly: false)
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

            services.Scan(scan => scan.FromAssembliesOf(typeof(DependencyInjection))
                .AddClasses(classes => classes.AssignableTo(typeof(IDomainEventHandler<>)), publicOnly: false)
                   .AsImplementedInterfaces()
                   .WithScopedLifetime());

            services.TryDecorate(typeof(ICommandHandler<>), typeof(ExceptionHandlingDecorator.CommandHandler<>));
            services.TryDecorate(typeof(ICommandHandler<,>), typeof(ExceptionHandlingDecorator.CommandHandler<,>));
            services.TryDecorate(typeof(IQueryHandler<,>), typeof(ExceptionHandlingDecorator.QueryHandler<,>));

            return services;
        }
    }
}
