using System.ComponentModel.DataAnnotations.Schema;
using ArtworkSharingPlatform.Domain.Entities.Abstract;
using ArtworkSharingPlatform.Domain.Entities.Artworks;
using ArtworkSharingPlatform.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace ArtworkSharingPlatform.Domain.Entities.Commissions;

public class CommissionRequest : BaseEntity
{
    [Column(TypeName = "decimal(18,2)")] public decimal MinPrice { get; set; }
    [Column(TypeName = "decimal(18,2)")] public decimal MaxPrice { get; set; }
    [Column(TypeName = "decimal(18,2)")] public decimal ActualPrice { get; set; }
    public string? RequestDescription { get; set; }
    public string? NotAcceptedReason { get; set; }
    public DateTime RequestDate { get; set; }
    public byte IsProgressStatus { get; set; }
    public int SenderId { get; set; }
    
    public int ReceiverId { get; set; }
    
    public int GenreId { get; set; }
    
    public int CommissionStatusId { get; set; }
    public User? Sender { get; set; }
    
    public User? Receiver { get; set; }
    
    public Genre? Genre { get; set; }
    public CommissionStatus? CommissionStatus { get; set; }
    public List<CommissionImage>? CommissionImages { get; set; }
}