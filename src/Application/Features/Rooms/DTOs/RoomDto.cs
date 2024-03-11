// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Rooms.DTOs;

[Description("Rooms")]
public class RoomDto
{
    [Description("Id")] public int Id { get; set; }

    [Description("Room Name")] public string? Name { get; set; }
    [Description("Room Status")]public string? RoomStatus {get; set;}
    [Description("Room Images")] public List<RoomImage>? RoomImages { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Room, RoomDto>().ReverseMap();
        }
    }
}