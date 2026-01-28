using MeetingSystem.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Companies.Delete
{
    public sealed record DeleteCompanyCommand(Guid UserId, Guid CompanyId) : ICommand;
}
