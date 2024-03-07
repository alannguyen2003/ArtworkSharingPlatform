using System.ComponentModel.DataAnnotations.Schema;
using ArtworkSharingPlatform.Domain.Entities.Abstract;
using ArtworkSharingPlatform.Domain.Entities.Artworks;
using ArtworkSharingPlatform.Domain.Entities.Users;

namespace ArtworkSharingPlatform.Domain.Entities.Orders;

public class PreOrder : BaseEntity
{
    public int ArtworkId { get; set; }
    public DateTime EstimateDate { get; set; }
    [Column(TypeName = "decimal(10,2)")] public decimal TotalPrice { get; set; }
    public Artwork? Artwork { get; set; }
    public User? Buyer { get; set; }
}