namespace CleanArchitecture.Blazor.Application.Features.RoomTypes.Specifications;
#nullable disable warnings
public class RoomTypeAdvancedSpecification : Specification<RoomType>
{
    public RoomTypeAdvancedSpecification(RoomTypeAdvancedFilter filter)
    {
        var today = DateTime.Now.ToUniversalTime().Date;
        var start = Convert.ToDateTime(today.ToString("yyyy-MM-dd", CultureInfo.CurrentCulture) + " 00:00:00",
            CultureInfo.CurrentCulture);
        var end = Convert.ToDateTime(today.ToString("yyyy-MM-dd", CultureInfo.CurrentCulture) + " 23:59:59",
            CultureInfo.CurrentCulture);
        var last30day = Convert.ToDateTime(
            today.AddDays(-30).ToString("yyyy-MM-dd", CultureInfo.CurrentCulture) + " 00:00:00",
            CultureInfo.CurrentCulture);
        Query.Where(x => x.Name != null)
            .Where(x => x.CreatedBy == filter.CurrentUser.UserId, filter.ListView == RoomTypeListView.My)
            .Where(x => x.Created >= start && x.Created <= end, filter.ListView == RoomTypeListView.CreatedToday)
            .Where(x => x.Created >= last30day, filter.ListView == RoomTypeListView.Created30Days);

    }
}