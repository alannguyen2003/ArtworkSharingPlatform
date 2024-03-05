using ArtworkSharingPlatform.Domain.Entities.Abstract;

namespace ArtworkSharingPlatform.Domain.Entities.Commissions;

public class CommissionStatus:BaseEntity
{
    public string? Description { get; set; }
    public List<CommissionRequest>? CommissionHistories { get; set; }
}