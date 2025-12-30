using MeetingSystem.Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Companies.CreateCompany
{
    public sealed record CreateCompanyCommand(Guid ManagerId, string Name, string Description,
        IFormFile? Image, string Number, string Street, string City, string State) : ICommand<Guid>;
}
