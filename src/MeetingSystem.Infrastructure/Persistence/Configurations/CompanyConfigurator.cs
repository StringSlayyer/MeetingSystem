using MeetingSystem.Domain.Companies;
using MeetingSystem.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Infrastructure.Persistence.Configurations
{
    public class CompanyConfigurator : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Companies");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.OwnsOne(c => c.Address, address =>
            {
                address.Property(a => a.Street).HasColumnName("Street").HasMaxLength(100);
                address.Property(a => a.City).HasColumnName("City").HasMaxLength(50);
                address.Property(a => a.State).HasColumnName("State").HasMaxLength(50);
                address.Property(a => a.Number).HasColumnName("Number").HasMaxLength(20);
            });

            builder.HasOne(c => c.Manager)
                .WithMany()
                .HasForeignKey(c => c.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Rooms)
                .WithOne()
                .HasForeignKey(r => r.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(c => c.Rooms)
               .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
