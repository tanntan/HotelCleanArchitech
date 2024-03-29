using CleanArchitecture.Blazor.Application.Common.Security;

namespace CleanArchitecture.Blazor.Application.Features.Bookings.Specifications;

public class BookingAdvancedFilter : PaginationFilter
{
    public string? Name { get; set; }
    public BookingListView ListView { get; set; } =
        BookingListView.All; //<-- When the user selects a different ListView,

    public UserProfile?
        CurrentUser { get; set; } // <-- This CurrentUser property gets its value from the information of
}