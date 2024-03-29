using CleanArchitecture.Blazor.Domain.Common.Entities;
using CleanArchitecture.Blazor.Domain.Common.Enums;

namespace CleanArchitecture.Blazor.Domain.Entities;
public class Booking : BaseAuditableEntity {
    public int? GuestId {get; set;}
    public Guest? Guest {get; set;}
    public BookingType BookingType {get; set;} = default;
    public List<Room>? Rooms {get; set;}
    public DateTime CheckOutDate {get; set;}
    public DateTime CheckInDate {get; set;}
    public decimal TotalPrice {get; set;}
}