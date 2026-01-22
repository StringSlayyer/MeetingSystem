using MeetingSystem.Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MeetingSystem.Application.Companies.Update
{
    public sealed record UpdateCompanyCommand(
        Guid CompanyId,
        Guid ManagerId,
        string Name,
        string Description,
        IFormFile? Image,
        string Number,
        string Street,
        string City,
        string State) : ICommand<Guid>;
}
