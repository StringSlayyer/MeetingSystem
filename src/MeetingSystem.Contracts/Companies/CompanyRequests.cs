using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Contracts.Companies
{
    public sealed record GetCompaniesRequest(int Page, int PageSize, string? SearchTerm);
    public sealed record GetCompanyByIdRequest(Guid CompanyId);
    public sealed record GetCompaniesByUserRequest(int Page, int PageSize);

}
