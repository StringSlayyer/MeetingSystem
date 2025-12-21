using MeetingSystem.Application.Abstractions.Data;
using MeetingSystem.Domain.Companies;
using MeetingSystem.Domain.Reservations;
using MeetingSystem.Domain.Resources;
using MeetingSystem.Domain.Users;
using MeetingSystem.Infrastructure.DomainEvents;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Infrastructure.Persistence
{
    public sealed class ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IDomainEventsDispatcher domainEventsDispatcher)
        : DbContext(options), IApplicationDbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<Reservation> Reservations { get; set; }
   
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await PublishDomainEventsAsync();

            int result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        private async Task PublishDomainEventsAsync()
        {
            var domainEvents = ChangeTracker
                .Entries<Entity>()
                .Select(entry => entry.Entity)
                .SelectMany(entity =>
                {
                    List<IDomainEvent> domainEvents = entity.DomainEvents;

                    entity.ClearDomainEvents();

                    return domainEvents;
                })
                .ToList();

            await domainEventsDispatcher.DispatchAsync(domainEvents);
        }

    }

}
