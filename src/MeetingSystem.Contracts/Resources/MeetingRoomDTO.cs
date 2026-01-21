using MeetingSystem.Contracts.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Contracts.Resources
{
    public sealed record MeetingRoomDTO(Guid Id, Guid CompanyId, string Name,
        string Description, decimal PricePerHour,
        string? ImageUrl, int Capacity, List<string> Features)
        : ResourceDTO(Id, CompanyId, Name, Description, PricePerHour, ImageUrl, Capacity, Features);
}
