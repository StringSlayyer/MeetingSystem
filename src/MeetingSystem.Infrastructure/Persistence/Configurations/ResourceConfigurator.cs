using MeetingSystem.Domain.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace MeetingSystem.Infrastructure.Persistence.Configurations
{
    public class ResourceConfigurator : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder.ToTable("Resources");
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name).IsRequired().HasMaxLength(100);
            builder.Property(r => r.Description).HasMaxLength(1000); 
            builder.Property(r => r.ImageUrl).HasMaxLength(500);
            builder.Property(r => r.PricePerHour).HasColumnType("decimal(18,2)");
            builder.Property(r => r.Capacity).IsRequired();

            builder.Property(r => r.RowVersion).IsRowVersion();

            var featuresConverter = new ValueConverter<List<string>, string>(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null) ?? new List<string>()
            );

            
            var featuresComparer = new ValueComparer<List<string>>(
                (c1, c2) => c1.SequenceEqual(c2), 
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())), 
                c => c.ToList() 
            );

            builder.Property(r => r.Features)
                .HasConversion(featuresConverter)
                .Metadata.SetValueComparer(featuresComparer);

            builder.Property(r => r.Features)
                .HasColumnName("Features")
                .IsRequired();

            builder.HasDiscriminator<string>("ResourceType")
                .HasValue<MeetingRoom>("MeetingRoom")
                .HasValue<ParkingSpot>("ParkingSpot");
                
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
