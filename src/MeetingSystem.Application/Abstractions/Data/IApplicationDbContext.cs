using MeetingSystem.Domain.Companies;
using MeetingSystem.Domain.Reservations;
using MeetingSystem.Domain.Resources;
using MeetingSystem.Domain.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Abstractions.Data
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Company> Companies { get; }
        DbSet<Resource> Resources { get; }
        DbSet<Reservation> Reservations { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
