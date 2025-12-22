using MeetingSystem.Application.Abstractions.Messaging;
using MeetingSystem.Contracts.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Resources.GetByCompany
{
    public sealed record GetResourcesByCompanyQuery(Guid CompanyId) : IQuery<List<ResourceDTO>>;
}
