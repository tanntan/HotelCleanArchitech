using CleanArchitecture.Blazor.Domain.Common.Entities;

namespace CleanArchitecture.Blazor.Domain.Entities;
public class Guest : BaseAuditableEntity {
    public string? FirstName {get; set;}
    public string? LastName {get; set;}
    public DateTime? DateOfBith {get; set;}
    public string? Address {get; set;}
    public string? Nationality {get; set;}
    public string? Phone {get; set;}
    public string? Email {get; set;}
    public IdentityImage? ProfilePicture {get; set;}
    public IdentityImage? IdentityImage {get; set;}
}
public class IdentityImage
{
    public required string Name { get; set; }
    public decimal Size { get; set; }
    public required string Url { get; set; }
}