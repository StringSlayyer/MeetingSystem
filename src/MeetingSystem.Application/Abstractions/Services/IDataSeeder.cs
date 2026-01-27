using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Abstractions.Services
{
    public interface IDataSeeder
    {
        Task SeedAsync(CancellationToken cancellationToken);
    }
}
