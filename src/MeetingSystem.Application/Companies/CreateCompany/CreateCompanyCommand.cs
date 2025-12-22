using MeetingSystem.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Companies.CreateCompany
{
    public sealed record CreateCompanyCommand(Guid ManagerId, string Name, string Number,
        string Street, string City, string State) : ICommand<Guid>;
}
