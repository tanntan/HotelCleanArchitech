using CleanArchitecture.Blazor.Application.Common.Security;

namespace CleanArchitecture.Blazor.Application.Features.RoomTypes.Specifications;

public class RoomTypeAdvancedFilter : PaginationFilter
{
    public string? Name { get; set; }
    public decimal? PricePerNight {get; set;}
    public RoomTypeListView ListView { get; set; } =
        RoomTypeListView.All; //<-- When the user selects a different ListView,

    public UserProfile?
        CurrentUser { get; set; } // <-- This CurrentUser property gets its value from the information of
}