namespace CleanArchitecture.Blazor.Application.Features.Guests.Specifications;
#nullable disable warnings
public class GuestAdvancedSpecification : Specification<Guest>
{
    public GuestAdvancedSpecification(GuestAdvancedFilter filter)
    {
        var today = DateTime.Now.ToUniversalTime().Date;
        var start = Convert.ToDateTime(today.ToString("yyyy-MM-dd", CultureInfo.CurrentCulture) + " 00:00:00",
            CultureInfo.CurrentCulture);
        var end = Convert.ToDateTime(today.ToString("yyyy-MM-dd", CultureInfo.CurrentCulture) + " 23:59:59",
            CultureInfo.CurrentCulture);
        var last30day = Convert.ToDateTime(
            today.AddDays(-30).ToString("yyyy-MM-dd", CultureInfo.CurrentCulture) + " 00:00:00",
            CultureInfo.CurrentCulture);
        Query.Where(x => x.FirstName != null)
            .Where(x => x.CreatedBy == filter.CurrentUser.UserId, filter.ListView == GuestListView.My)
            .Where(x => x.Created >= start && x.Created <= end, filter.ListView == GuestListView.CreatedToday)
            .Where(x => x.Created >= last30day, filter.ListView == GuestListView.Created30Days);

    }
}