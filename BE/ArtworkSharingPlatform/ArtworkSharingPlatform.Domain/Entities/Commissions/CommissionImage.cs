using ArtworkSharingPlatform.Domain.Entities.Abstract;

namespace ArtworkSharingPlatform.Domain.Entities.Commissions;

public class CommissionImage : BaseEntity
{
    public string? Url { get; set; }
    public bool IsThumbnail { get; set; }
    public string? PublicId { get; set; }
    public DateTime CreatedDate { get; set; }
    public int CommissionRequestId { get; set; }
    public CommissionRequest? CommissionRequest { get; set; }
}