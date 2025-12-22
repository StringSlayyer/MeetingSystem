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

        
    }
}
