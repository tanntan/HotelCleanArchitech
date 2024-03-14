
namespace CleanArchitecture.Blazor.Application.Features.Guests.Specifications;

public enum GuestListView
{
    [Description("All")] All,
    [Description("My Guest")] My,
    [Description("Created Toady")] CreatedToday,

    [Description("Created within the last 30 days")]
    Created30Days
}