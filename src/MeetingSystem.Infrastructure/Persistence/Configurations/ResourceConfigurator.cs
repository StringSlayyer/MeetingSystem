using MeetingSystem.Domain.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Infrastructure.Persistence.Configurations
{
    public class ResourceConfigurator : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder.ToTable("Resources");
            builder.HasKey(r => r.Id);

            builder.Property(r => r.RowVersion).IsRowVersion();

            builder.HasDiscriminator<string>("ResourceType")
                .HasValue<MeetingRoom>("MeetingRoom")
                .HasValue<ParkingSpot>("ParkingSpot");
                
        }
    }

    public class MeetingRoomConfiguration : IEntityTypeConfiguration<MeetingRoom>
    {
        public void Configure(EntityTypeBuilder<MeetingRoom> builder)
        {
            builder.Property(m => m.Seats).IsRequired();
        }
    }

    public class ParkingSpotConfiguration : IEntityTypeConfiguration<ParkingSpot>
    {
        public void Configure(EntityTypeBuilder<ParkingSpot> builder)
        {
            builder.Property(p => p.IsCovered).IsRequired();
        }
    }
}
