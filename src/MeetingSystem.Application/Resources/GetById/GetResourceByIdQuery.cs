using MeetingSystem.Application.Abstractions.Messaging;
using MeetingSystem.Contracts.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Resources.GetById
{
    public sealed record GetResourceByIdQuery(Guid Id) : IQuery<ResourceDTO>;
}
