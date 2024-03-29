namespace CleanArchitecture.Blazor.Application.Features.Bookings.Specifications;
#nullable disable warnings
public class BookingAdvancedSpecification : Specification<Booking>
{
    public BookingAdvancedSpecification(BookingAdvancedFilter filter)
    {
        var today = DateTime.Now.ToUniversalTime().Date;
        var start = Convert.ToDateTime(today.ToString("yyyy-MM-dd", CultureInfo.CurrentCulture) + " 00:00:00",
            CultureInfo.CurrentCulture);
        var end = Convert.ToDateTime(today.ToString("yyyy-MM-dd", CultureInfo.CurrentCulture) + " 23:59:59",
            CultureInfo.CurrentCulture);
        var last30day = Convert.ToDateTime(
            today.AddDays(-30).ToString("yyyy-MM-dd", CultureInfo.CurrentCulture) + " 00:00:00",
            CultureInfo.CurrentCulture);
        Query
            .Where(x => x.CreatedBy == filter.CurrentUser.UserId, filter.ListView == BookingListView.My)
            .Where(x => x.Created >= start && x.Created <= end, filter.ListView == BookingListView.CreatedToday)
            .Where(x => x.Created >= last30day, filter.ListView == BookingListView.Created30Days);

    }
}