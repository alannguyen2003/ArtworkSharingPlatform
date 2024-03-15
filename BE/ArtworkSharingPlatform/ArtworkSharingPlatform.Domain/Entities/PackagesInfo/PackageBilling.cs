using System.ComponentModel.DataAnnotations.Schema;
using ArtworkSharingPlatform.Domain.Entities.Abstract;
using ArtworkSharingPlatform.Domain.Entities.Users;

namespace ArtworkSharingPlatform.Domain.Entities.PackagesInfo;

public class PackageBilling : BaseEntity
{
    [Column(TypeName = "decimal(10,2)")] public decimal TotalPrice { get; set; }
    public string? Status { get; set; }
    public ICollection<PackageInformation>? PackageInformation { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
}