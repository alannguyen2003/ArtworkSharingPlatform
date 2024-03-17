using System.ComponentModel.DataAnnotations.Schema;
using ArtworkSharingPlatform.Domain.Entities.Abstract;
using ArtworkSharingPlatform.Domain.Entities.Orders;
using ArtworkSharingPlatform.Domain.Entities.Users;

namespace ArtworkSharingPlatform.Domain.Entities.Artworks;

public class Artwork : BaseEntity
{
    public string Title { get; set; }
    public string? Description { get; set; }
    [Column(TypeName = "decimal(18,2)")] public decimal Price { get; set; }
    public int ReleaseCount { get; set; }
    public int GenreId { get; set; }
    public int OwnerId { get; set; }
    public User Owner { get; set; }
    public DateTime CreatedDate { get; set; }
    public byte Status { get; set; }
    public PreOrder? PreOrder { get; set; }
    public ICollection<ArtworkImage> ArtworkImages { get; set; }
    public ICollection<Like>? Likes { get; set; }
    public ICollection<Comment>? Comments { get; set; }
    public ICollection<Rating>? Ratings { get; set; }
    public ICollection<Report> Reports { get; set; }
    public Genre Genre { get; set; }
    public List<Purchase> Purchases { get; set; }
}