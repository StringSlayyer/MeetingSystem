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
                MeetingRoom room => new MeetingRoomDTO(room.Id, room.CompanyId, room.Name, room.Seats),
                ParkingSpot spot => new ParkingSpotDTO(spot.Id, spot.CompanyId, spot.Name, spot.IsCovered),
                _ => throw new NotImplementedException($"Resource type {resource.GetType().Name} not mapped")
            };
        }
    }
}
