using MeetingSystem.Application.Abstractions.Messaging;
using MeetingSystem.Contracts;
using MeetingSystem.Contracts.Companies;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Companies.GetCompanies
{
    public sealed record GetCompaniesQuery(
        int Page = 1, 
        int PageSize = 10, 
        string? SearchTerm = null
        ) : IQuery<PagedResult<CompanyDTO>>;
}
