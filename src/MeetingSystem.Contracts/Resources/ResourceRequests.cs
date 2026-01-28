using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Contracts.Resources
{
    public sealed record GetResourcesByCompanyRequest(Guid CompanyId);
    public sealed record GetResourceByIdRequest(Guid Id);

}
