using System;
using System.Collections.Generic;
using System.Text;

namespace SharedKernel
{
    public interface IDomainEventHandler<in T> where T : IDomainEvent
    {
        Task Handle(T domainEvent, CancellationToken cancellationToken);
    }
}
