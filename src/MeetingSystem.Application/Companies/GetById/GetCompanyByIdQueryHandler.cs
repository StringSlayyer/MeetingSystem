using MeetingSystem.Application.Abstractions.Data;
using MeetingSystem.Application.Abstractions.Messaging;
using MeetingSystem.Application.Extensions;
using MeetingSystem.Contracts.Companies;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Companies.GetById
{
    public sealed class GetCompanyByIdQueryHandler(IApplicationDbContext context)
        : IQueryHandler<GetCompanyByIdQuery, SingleCompanyDTO>
    {
        public async Task<Result<SingleCompanyDTO>> Handle(GetCompanyByIdQuery query, CancellationToken cancellationToken)
        {

            var company = await context.Companies.AsNoTracking()
                .Where(c => c.Id == query.Id)
                .Select(c => new SingleCompanyDTO(
                    c.Id,
                    c.Name,
                    c.Description,
                    c.ImageUrl,
                    new ManagerDTO(c.Manager.Id, c.Manager.FirstName, c.Manager.LastName),
                    c.Address.Number,
                    c.Address.Street,
                    c.Address.City,
                    c.Address.State,
                    c.Rating,
                    c.BookingCount,
                    c.Rooms.Select(r => r.ToDTO()).ToList())).FirstOrDefaultAsync(cancellationToken);

            return company;
        }
    }
}
