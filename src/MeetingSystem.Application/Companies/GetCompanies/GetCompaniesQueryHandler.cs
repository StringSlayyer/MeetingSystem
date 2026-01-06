using MeetingSystem.Application.Abstractions.Data;
using MeetingSystem.Application.Abstractions.Messaging;
using MeetingSystem.Contracts;
using MeetingSystem.Contracts.Companies;
using MeetingSystem.Domain.Companies;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Companies.GetCompanies
{
    public sealed class GetCompaniesQueryHandler(IApplicationDbContext context)
        : IQueryHandler<GetCompaniesQuery, PagedResult<CompanyDTO>>
    {
        public async Task<Result<PagedResult<CompanyDTO>>> Handle(GetCompaniesQuery query, CancellationToken cancellationToken)
        {
            var dbQuery = context.Companies.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                dbQuery = dbQuery.Where(c =>
                    c.Name.Contains(query.SearchTerm) ||
                    c.Address.City.Contains(query.SearchTerm));
            }

            var totalCount = await dbQuery.CountAsync(cancellationToken);

            var items = await dbQuery
                .OrderBy(c => c.Name) 
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .Select(c => new CompanyDTO(
                    c.Id,
                    c.Name,
                    c.Description,
                    c.ImageUrl,
                    c.Address.City,
                    c.Address.State,
                    c.Rating,
                    c.BookingCount
                ))
            .ToListAsync(cancellationToken);

            return new PagedResult<CompanyDTO> { Items = items, TotalCount = totalCount };
        }
    }
}
