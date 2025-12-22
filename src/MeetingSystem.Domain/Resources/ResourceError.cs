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
    }
}
