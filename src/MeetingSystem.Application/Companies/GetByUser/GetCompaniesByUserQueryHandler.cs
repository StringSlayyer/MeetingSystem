using MeetingSystem.Application.Abstractions.Data;
using MeetingSystem.Application.Abstractions.Messaging;
using MeetingSystem.Contracts;
using MeetingSystem.Contracts.Companies;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Companies.GetByUser
{
    public sealed class GetCompaniesByUserQueryHandler(IApplicationDbContext context)
        : IQueryHandler<GetCompaniesByUserQuery, PagedResult<CompanyDTO>>
    {
        public async Task<Result<PagedResult<CompanyDTO>>> Handle(GetCompaniesByUserQuery query, CancellationToken cancellationToken)
        {
            var dbQuery = context.Companies.AsNoTracking().Where(c => c.ManagerId == query.UserId);

           

            var totalCount = await dbQuery.CountAsync(cancellationToken);

            if(totalCount == 0)
            {
                return new PagedResult<CompanyDTO> { Items = new(), TotalCount = totalCount };
            }

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
