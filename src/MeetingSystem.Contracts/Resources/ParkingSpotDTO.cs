using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Contracts.Resources;

public sealed record ParkingSpotDTO(Guid Id, Guid CompanyId, string Name, bool IsCovered)
    : ResourceDTO(Id, CompanyId, Name);
