using ArtworkSharingPlatform.Domain.Entities.Abstract;
using ArtworkSharingPlatform.Domain.Entities.PackagesInfo;
using ArtworkSharingPlatform.Domain.Entities.Users;

namespace ArtworkSharingPlatform.Domain.Entities.Transactions;

public class Transaction : BaseEntity
{
    public string? ReportName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime CreateDate { get; set; }
    public ICollection<PackageBilling>? PackageBillings { get; set; }
    public User? Manager { get; set; }
}