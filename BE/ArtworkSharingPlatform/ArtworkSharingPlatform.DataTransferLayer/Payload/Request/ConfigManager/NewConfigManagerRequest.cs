using System.Text.Json.Serialization;

namespace ArtworkSharingPlatform.DataTransferLayer.Payload.Request.ConfigManager;

public class NewConfigManagerRequest
{
    public bool IsServicePackageConfig { get; set; }
    public bool IsPhysicalImageConfig { get; set; }
    public bool IsGeneralConfig { get; set; }
    public string? LogoUrl { get; set; }
    public string? MyPhoneNumber { get; set; }
    public string? Address { get; set; }
    public bool IsPagingConfig { get; set; }
    public int TotalItemPerPage { get; set; }
    public int RowSize { get; set; }
    public bool IsAdvertisementConfig { get; set; }
    public string? CompanyName { get; set; }
    public string? CompanyPhoneNumber { get; set; }
    public string? CompanyEmail { get; set; }
    [JsonIgnore]
    public int AdministratorId { get; set; }
}