using System.ComponentModel.DataAnnotations.Schema;
using ArtworkSharingPlatform.Domain.Entities.Abstract;
using ArtworkSharingPlatform.Domain.Entities.Users;

namespace ArtworkSharingPlatform.Domain.Entities.Packages;

public class PackageBilling : BaseEntity
{
    [Column(TypeName = "decimal(10,2)")] public decimal TotalPrice { get; set; }
    public string? Status { get; set; }
    public ICollection<PackageInformation>? PackageInformation { get; set; }
    public User? User { get; set; }
}