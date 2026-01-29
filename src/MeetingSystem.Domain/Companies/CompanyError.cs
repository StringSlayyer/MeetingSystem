using System;
using System.Collections.Generic;
using System.Text;
using SharedKernel;

namespace MeetingSystem.Domain.Companies
{
    public static class CompanyError
    {
        public static Error NoCompanies => Error.NotFound(
            "Companies.NoCompanies", "No companies were found");

        public static Error CompanyNotFound(Guid id) => Error.NotFound(
            "Companis.CompanyNotFound", $"Company with id {id} was not found");

        public static Error Unauthorized => Error.Failure("Company.Unauthorized",
            "You are not the manager of this company");

        public static Error HasActiveBookings => Error.Conflict(
                    "Company.HasActiveBookings",
                    "Cannot delete company. There are upcoming reservations for your resources.");



    }
}
