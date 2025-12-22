using MeetingSystem.Application.Abstractions.Data;
using MeetingSystem.Application.Abstractions.Messaging;
using MeetingSystem.Domain.Companies;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Companies.GetCompanies
{
    public sealed class GetCompaniesQueryHandler(IApplicationDbContext context)
        : IQueryHandler<GetCompaniesQuery, List<CompanyDTO>>
    {
        public async Task<Result<List<CompanyDTO>>> Handle(GetCompaniesQuery query, CancellationToken cancellationToken)
        {
            return await context.Companies
                .AsNoTracking()
                .Select(c => new CompanyDTO(c.Id, c.ManagerId, c.Name, c.Address.Number, c.Address.Street, c.Address.City, c.Address.State, c.Rooms.Count)).ToListAsync(cancellationToken);
        }
    }
}
