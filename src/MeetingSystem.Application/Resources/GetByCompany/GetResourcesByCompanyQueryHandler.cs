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

namespace MeetingSystem.Application.Resources.GetByCompany
{
    public sealed class GetResourcesByCompanyQueryHandler(IApplicationDbContext context)
        : IQueryHandler<GetResourcesByCompanyQuery, List<ResourceDTO>>
    {
        public async Task<Result<List<ResourceDTO>>> Handle(GetResourcesByCompanyQuery query, CancellationToken cancellationToken)
        {
            var resources = await context.Resources
                .Where(r => r.CompanyId == query.CompanyId)
                .AsNoTracking()
                .ToListAsync();

            var dtos = resources.Select(r => r.ToDTO());
            return dtos.ToList();
            
        }

       
    }
}
