using MeetingSystem.Contracts.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Contracts.Resources
{
    public sealed record MeetingRoomDTO(Guid Id, Guid CompanyId, string Name, int Seats)
        : ResourceDTO(Id, CompanyId, Name);
}
