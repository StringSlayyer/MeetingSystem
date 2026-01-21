using MeetingSystem.Application.Abstractions.Messaging;
using MeetingSystem.Contracts;
using MeetingSystem.Contracts.Companies;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Companies.GetByUser
{
    public sealed record GetCompaniesByUserQuery(Guid UserId,
        int Page = 1,
        int PageSize = 10) : IQuery<PagedResult<CompanyDTO>>;
}
