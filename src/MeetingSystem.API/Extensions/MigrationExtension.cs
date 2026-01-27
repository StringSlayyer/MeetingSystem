using MeetingSystem.Application.Abstractions.Services;
using MeetingSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace MeetingSystem.API.Extensions
{
    public static class MigrationExtension
    {
        public static async Task InitializeDatabaseAsync(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var seeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();

            await dbContext.Database.MigrateAsync();
            await seeder.SeedAsync(CancellationToken.None);
        }
    }
}
