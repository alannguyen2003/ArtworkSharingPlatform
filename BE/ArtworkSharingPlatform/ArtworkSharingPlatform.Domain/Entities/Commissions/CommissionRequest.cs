using System.ComponentModel.DataAnnotations.Schema;
using ArtworkSharingPlatform.Domain.Entities.Abstract;
using ArtworkSharingPlatform.Domain.Entities.Artworks;
using ArtworkSharingPlatform.Domain.Entities.Users;

namespace ArtworkSharingPlatform.Domain.Entities.Commissions;

public class CommissionRequest : BaseEntity
{
    [Column(TypeName = "decimal(10,5)")] public decimal MinPrice { get; set; }
    public string? RequestDescription { get; set; }
    public string? NotAcceptedReason { get; set; }
    public DateTime RequestDate { get; set; }

    public byte Status { get; set; }
    public User? Sender { get; set; }

    public User? Receiver { get; set; }
    public int GenreId { get; set; }
    public Genre? Genre { get; set; }
    public CommissionStatus? CommissionStatus { get; set; }
    public List<CommissionImage>? CommissionImages { get; set; }
}