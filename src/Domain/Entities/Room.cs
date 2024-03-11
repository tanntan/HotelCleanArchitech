using CleanArchitecture.Blazor.Domain.Common.Entities;
namespace CleanArchitecture.Blazor.Domain.Entities
{
    public class Room : BaseAuditableEntity
    {
        public string? Name { get; set; }
        public string? RoomStatus { get; set; }
        public List<RoomImage>? RoomImages { get; set; }
        public int RoomTypeId {get; set;}
        public RoomType? RoomType {get; set;}
    }

    public class RoomImage
    {
        public required string Name { get; set; }
        public decimal Size { get; set; }
        public required string Url { get; set; }
    }
}

