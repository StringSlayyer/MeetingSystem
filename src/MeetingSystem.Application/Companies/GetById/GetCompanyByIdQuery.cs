using MeetingSystem.Application.Abstractions.Messaging;
using MeetingSystem.Contracts.Companies;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Companies.GetById
{
    public sealed record GetCompanyByIdQuery(Guid Id) : IQuery<SingleCompanyDTO>;
}
