using CleanArchitecture.Blazor.Domain.Common.Entities;

namespace CleanArchitecture.Blazor.Domain.Entities
{
    public class RoomType : BaseAuditableEntity {
        public string? Name {get; set;}
        public string? Description {get; set;}
        public decimal? PricePerNight {get; set;}
        public int Capacity {get; set;}
        public List<Room>? Rooms {get; set;}
        public string? Status {get; set;}

    }
}
