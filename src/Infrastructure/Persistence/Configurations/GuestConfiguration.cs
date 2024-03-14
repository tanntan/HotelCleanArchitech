// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Text.Json;
using CleanArchitecture.Blazor.Application.Common.Interfaces.Serialization;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Blazor.Infrastructure.Persistence.Configurations;
#nullable disable
public class GuestConfiguration : IEntityTypeConfiguration<Guest>
{
    public void Configure(EntityTypeBuilder<Guest> builder)
    {
        builder.Ignore(e => e.DomainEvents);
        builder.Property(e => e.IdentityImage)
            .HasConversion(
                v => JsonSerializer.Serialize(v, DefaultJsonSerializerOptions.Options),
                v => JsonSerializer.Deserialize<IdentityImage>(v, DefaultJsonSerializerOptions.Options),
                new ValueComparer<IdentityImage>(
                    (c1, c2) => c1.Equals(c2),
                    c => c.GetHashCode()
            ));
        builder.Property(e => e.ProfilePicture)
            .HasConversion(
                v => JsonSerializer.Serialize(v, DefaultJsonSerializerOptions.Options),
                v => JsonSerializer.Deserialize<IdentityImage>(v, DefaultJsonSerializerOptions.Options),
                new ValueComparer<IdentityImage>(
                    (c1, c2) => c1.Equals(c2),
                    c => c.GetHashCode()
            ));
    }
}