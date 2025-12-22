using MeetingSystem.Domain.Reservations;
using MeetingSystem.Domain.Resources;
using MeetingSystem.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Infrastructure.Persistence.Configurations
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.ToTable("Reservations");
            builder.HasKey(r => r.Id);

            builder.OwnsOne(r => r.TimeSlot, ts =>
            {
                ts.Property(x => x.Start).HasColumnName("StartTime").IsRequired();
                ts.Property(x => x.End).HasColumnName("EndTime").IsRequired();
                ts.HasIndex(x => new { x.Start, x.End });
            });

            builder.Property(r => r.Status)
            .HasConversion(
                status => status.Id, // To DB: Save only the Int (e.g., 1)
                value => ReservationStatus.List().First(s => s.Id == value) // From DB: Find the static instance
            )
            .IsRequired();

            builder.HasOne<Resource>()
                .WithMany()
                .HasForeignKey(r => r.ResourceId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
