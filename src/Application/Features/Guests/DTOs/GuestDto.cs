// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Guests.DTOs;

[Description("Guest")]
public class GuestDto
{
    [Description("Id")] public int Id { get; set; }

    [Description("Guest FirstName")] public string? FirstName { get; set; }
    [Description("Guest LastName")] public string? LastName {get; set;}
    [Description("Guest Address")] public string? Address {get; set;}
    [Description("Guest Email")] public string? Email {get; set;}
    [Description("Guest Phone")] public string? Phone {get; set;}
    [Description("Profile Image")] public IdentityImage? ProfileImage {get; set;}
    [Description("Identity Image")] public IdentityImage? IdentityImage {get; set;}

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Guest, GuestDto>().ReverseMap();
        }
    }
}