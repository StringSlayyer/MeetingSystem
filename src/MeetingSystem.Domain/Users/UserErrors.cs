using SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Domain.Users
{
    public static class UserErrors
    {
        public static Error EmailNotUnique = Error.Conflict(
            "Users.EmailNotUnique", "User with this email already exists");
        public static Error NotFoundByEmail = Error.NotFound(
            "Users.NotFoundByEmail", "No user found with the provided email");

        public static Error UserNotFound(Guid id) => Error.NotFound(
            "Users.NotFound", $"No user with id {id} was found");
    }
}
