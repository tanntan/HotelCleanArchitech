
using System;
using System.Text.Json;
using CleanArchitecture.Blazor.Application.Common.Interfaces.Serialization;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Blazor.Infrastructure.Persistence.Configurations
{
    public class RoomTypeConfiguratyion : IEntityTypeConfiguration<RoomType>
    {
        public void Configure(EntityTypeBuilder<RoomType> builder)
        {
            builder.Ignore(e => e.DomainEvents);
            builder.Property(e => e.Rooms)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, DefaultJsonSerializerOptions.Options),
                    v => JsonSerializer.Deserialize<List<Room>>(v, DefaultJsonSerializerOptions.Options),
                    new ValueComparer<List<Room>>(
                        (c1, c2) => c1!.SequenceEqual(c2!),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToList()));
                        
             
        }
    }
}

