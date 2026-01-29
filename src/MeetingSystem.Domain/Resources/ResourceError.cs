using SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Domain.Resources
{
    public sealed class ResourceError
    {
        public static Error UserNotOwner => Error.Failure(
            "Resources.UserNotOwner", "User is not owner of the company");

        public static Error NotFound(Guid id) => Error.NotFound(
            "Resources.NotFound", $"Resource with id {id} was not found");

        public static Error Unauthorized => Error.Failure(
            "Resource.Unauthorized", "You do not own this resource");

        public static Error HasFutureBookings => Error.Conflict(
                    "Resource.HasFutureBookings",
                    "Cannot delete this resource because it has upcoming reservations. Cancel them first.");

        public static Error MeetingRoomNotFound => Error.NotFound("Resource.NotFound",
            "Meeting room not found");

        public static Error ParkingSpotNotFound => Error.NotFound(
            "Resource.NotFound", "Parking spot not found");
    }
}
