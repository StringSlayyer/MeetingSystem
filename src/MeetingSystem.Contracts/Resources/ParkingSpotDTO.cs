using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Contracts.Resources;

public sealed record ParkingSpotDTO(Guid Id, Guid CompanyId, string Name, bool IsCovered,
    string Description, decimal PricePerHour,
string? ImageUrl, int Capacity)
    : ResourceDTO(Id, CompanyId, Name, Description, PricePerHour, ImageUrl, Capacity, null);
