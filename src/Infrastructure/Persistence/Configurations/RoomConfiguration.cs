
using System;
using System.Text.Json;
using CleanArchitecture.Blazor.Application.Common.Interfaces.Serialization;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Blazor.Infrastructure.Persistence.Configurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.Ignore(e => e.DomainEvents);
            builder.Property(e => e.RoomImages)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, DefaultJsonSerializerOptions.Options),
                    v => JsonSerializer.Deserialize<List<RoomImage>>(v, DefaultJsonSerializerOptions.Options),
                    new ValueComparer<List<RoomImage>>(
                        (c1, c2) => c1.SequenceEqual(c2),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToList()));
                        
             
        }
    }
}

