using MeetingSystem.Application.Abstractions.Data;
using MeetingSystem.Application.Abstractions.Messaging;
using MeetingSystem.Application.Extensions;
using MeetingSystem.Contracts.Resources;
using MeetingSystem.Domain.Resources;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Resources.GetById
{
    public sealed class GetResourceByIdQueryHandler(IApplicationDbContext context)
        : IQueryHandler<GetResourceByIdQuery, ResourceDTO>
    {
        public async Task<Result<ResourceDTO>> Handle(GetResourceByIdQuery query, CancellationToken cancellationToken)
        {
            var resource = await context.Resources
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == query.Id, cancellationToken);

            if(resource is null)
            {
                return Result.Failure<ResourceDTO>(ResourceError.NotFound(query.Id));
            }

            return resource.ToDTO();
        }
    }
}
