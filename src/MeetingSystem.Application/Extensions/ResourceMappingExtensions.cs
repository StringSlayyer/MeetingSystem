using MeetingSystem.Contracts.Resources;
using MeetingSystem.Domain.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Extensions
{
    public static class ResourceMappingExtensions
    {
        public static ResourceDTO ToDTO(this Resource resource)
        {
            return resource switch
            {
                MeetingRoom room => new MeetingRoomDTO(room.Id, room.CompanyId, room.Name, room.Description, room.PricePerHour, room.ImageUrl, room.Capacity, room.Features),
                ParkingSpot spot => new ParkingSpotDTO(spot.Id, spot.CompanyId, spot.Name, spot.IsCovered, spot.Description, spot.PricePerHour, spot.ImageUrl, spot.Capacity),
                _ => throw new NotImplementedException($"Resource type {resource.GetType().Name} not mapped")
            };
        }
    }
}
