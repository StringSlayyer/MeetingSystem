using MeetingSystem.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Companies.GetCompanies
{
    public class GetCompaniesQuery() : IQuery<List<CompanyDTO>>;
}
