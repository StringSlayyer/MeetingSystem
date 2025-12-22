using SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Domain.Users
{
    public static class UserErrors
    {
        public static Error EmailNotUnique = Error.Conflict(
            "Users.EmailNotUnique", "The provided email was not unique");
        public static Error NotFoundByEmail = Error.NotFound(
            "Users.NotFoundByEmail", "No user found with the provided email");
    }
}
