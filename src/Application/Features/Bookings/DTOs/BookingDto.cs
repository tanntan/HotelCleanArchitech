using CleanArchitecture.Blazor.Application.Features.Guests.DTOs;
using CleanArchitecture.Blazor.Application.Features.Rooms.DTOs;

namespace CleanArchitecture.Blazor.Application.Features.Bookings.DTOs;

[Description("Bookings")]
public class BookingDto {
    public int Id {get; set;}
    public GuestDto? Guest {get; set;}
    public BookingType BookingType {get; set;} = default;
    public List<RoomDto>? Rooms {get; set;}
    [Description("Check Out Date")] public DateTime CheckOutDate {get; set;}
    [Description("Check In Date")] public DateTime CheckInDate {get; set;}
    [Description("Total")] public decimal TotalPrice {get; set;}

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Booking, BookingDto>().ReverseMap();
        }
    }
}