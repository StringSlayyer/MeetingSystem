using SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Abstractions.Messaging
{
    public interface IQueryHandler<in TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {
        Task<Result<TResponse>> Handle(TQuery query, CancellationToken cancellationToken);
    }
}
