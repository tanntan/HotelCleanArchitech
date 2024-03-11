
namespace CleanArchitecture.Blazor.Application.Features.RoomTypes.Specifications;

public enum RoomTypeListView
{
    [Description("All")] All,
    [Description("My Rooms")] My,
    [Description("Created Toady")] CreatedToday,

    [Description("Created within the last 30 days")]
    Created30Days
}