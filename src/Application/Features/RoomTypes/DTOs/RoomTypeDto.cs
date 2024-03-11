// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.RoomTypes.DTOs;

[Description("RoomType")]
public class RoomTypeDto
{
    [Description("Id")] public int Id { get; set; }

    [Description("Room Type Name")] public string? Name { get; set; }
    [Description("Description")] public string? Description {get; set;}
    [Description("Room Type Status")]public string? Status {get; set;}
    [Description("Price Per Night")] public decimal PricePerNight {get; set;}
    [Description("Capacity")] public int Capacity {get; set;}

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<RoomType, RoomTypeDto>().ReverseMap();
        }
    }
}