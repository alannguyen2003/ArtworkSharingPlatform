using System.ComponentModel.DataAnnotations.Schema;
using ArtworkSharingPlatform.Domain.Entities.Abstract;
using ArtworkSharingPlatform.Domain.Entities.Configs;

namespace ArtworkSharingPlatform.Domain.Entities.Packages;

public class PackageInformation : BaseEntity
{
    public int Credit { get; set; }
    [Column(TypeName = "decimal(10,5)")] public decimal Price { get; set; }
    public string? Status { get; set; }
    public ICollection<PackageBilling>? PackageBillings { get; set; }
    public ICollection<ConfigManager>? ConfigManagers { get; set; }
}