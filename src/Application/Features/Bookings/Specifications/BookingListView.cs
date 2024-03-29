
namespace CleanArchitecture.Blazor.Application.Features.Bookings.Specifications;

public enum BookingListView
{
    [Description("All")] All,
    [Description("My Booking")] My,
    [Description("Created Toady")] CreatedToday,

    [Description("Created within the last 30 days")]
    Created30Days
}