using ArtworkSharingPlatform.Domain.Entities.Abstract;
using ArtworkSharingPlatform.Domain.Entities.PackagesInfo;
using ArtworkSharingPlatform.Domain.Entities.Users;

namespace ArtworkSharingPlatform.Domain.Entities.Transactions;

public class Transaction : BaseEntity
{
    public string? ReportName { get; set; }    
    public DateTime CreateDate { get; set; }
    public decimal TotalPrice { get; set; }
    public int SenderId { get; set; }
    public User Sender { get; set; }
    public int? ReceiverId { get; set; }
    public User? Receiver { get; set; }
}