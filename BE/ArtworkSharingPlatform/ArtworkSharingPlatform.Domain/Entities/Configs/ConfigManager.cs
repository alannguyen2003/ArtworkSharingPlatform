using ArtworkSharingPlatform.Domain.Entities.Abstract;
using ArtworkSharingPlatform.Domain.Entities.PackagesInfo;
using ArtworkSharingPlatform.Domain.Entities.Users;

namespace ArtworkSharingPlatform.Domain.Entities.Configs;

public class ConfigManager : BaseEntity
{
    public DateTime ConfigDate { get; set; }
    public Boolean IsServicePackageConfig { get; set; }
    public Boolean IsPhysicalImageConfig { get; set; }
    public int MaxReleaseCount { get; set; }
    public Boolean IsGeneralConfig { get; set; }
    public string? LogoUrl { get; set; }
    public string? MyPhoneNumber { get; set; }
    public string? Address { get; set; }
    public Boolean IsPagingConfig { get; set; }
    public int TotalItemPerPage { get; set; }
    public int RowSize { get; set; }
    public Boolean IsAdvertisementConfig { get; set; }
    public string? CompanyName { get; set; }
    public string? CompanyPhoneNumber { get; set; }
    public string? CompanyEmail { get; set; }
    public User? Administrator { get; set; }
    public ICollection<PackageInformation>? PackageConfigs { get; set; }
}