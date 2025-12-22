using System;
using System.Collections.Generic;
using System.Text;
using SharedKernel;

namespace MeetingSystem.Domain.Companies
{
    public static class CompanyError
    {
        public static Error NoCompanies => Error.NotFound("Companies.NoCompanies", "No companies were found");
    }
}
