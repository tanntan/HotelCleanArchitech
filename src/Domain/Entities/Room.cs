using CleanArchitecture.Blazor.Domain.Common.Entities;
namespace CleanArchitecture.Blazor.Domain.Entities
{
    public class Room : BaseAuditableEntity
    {
        public string? Name { get; set; }
        public RoomStatus RoomStatus { get; set; } = default!;
        public List<RoomImage>? RoomImages { get; set; }
    }

    public enum RoomStatus
    {
        Active,
        Occupied,
        Ready,
        Dirty
    }

    public class RoomImage
    {
        public required string Name { get; set; }
        public decimal Size { get; set; }
        public required string Url { get; set; }
    }
}

