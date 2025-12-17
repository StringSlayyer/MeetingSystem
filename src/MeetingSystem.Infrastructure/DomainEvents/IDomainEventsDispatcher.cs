using SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Infrastructure.DomainEvents
{
    public interface IDomainEventsDispatcher
    {
        Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default);
    }
}
