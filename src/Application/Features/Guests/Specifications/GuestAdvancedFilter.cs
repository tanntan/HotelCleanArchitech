using CleanArchitecture.Blazor.Application.Common.Security;

namespace CleanArchitecture.Blazor.Application.Features.Guests.Specifications;

public class GuestAdvancedFilter : PaginationFilter
{
    public string? Name { get; set; }
    public GuestListView ListView { get; set; } =
        GuestListView.All; //<-- When the user selects a different ListView,

    public UserProfile?
        CurrentUser { get; set; } // <-- This CurrentUser property gets its value from the information of
}