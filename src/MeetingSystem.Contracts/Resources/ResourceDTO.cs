using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MeetingSystem.Contracts.Resources;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(MeetingRoomDTO), typeDiscriminator:"meetingRoom")]
[JsonDerivedType(typeof(ParkingSpotDTO), typeDiscriminator: "parkingSpot")]
public abstract record ResourceDTO(Guid Id, Guid CompanyId, Guid ManagerId, string Name,
    string Description, decimal PricePerHour,
        string? ImageUrl, int Capacity, List<string>? Features);
