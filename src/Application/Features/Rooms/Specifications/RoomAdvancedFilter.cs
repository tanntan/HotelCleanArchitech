using CleanArchitecture.Blazor.Application.Common.Security;

namespace CleanArchitecture.Blazor.Application.Features.Rooms.Specifications;

public class RoomAdvancedFilter : PaginationFilter
{
    public string? Name { get; set; }
    public RoomListView ListView { get; set; } =
        RoomListView.All; //<-- When the user selects a different ListView,

    public UserProfile?
        CurrentUser { get; set; } // <-- This CurrentUser property gets its value from the information of
}