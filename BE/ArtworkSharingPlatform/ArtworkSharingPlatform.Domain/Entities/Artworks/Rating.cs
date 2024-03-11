using ArtworkSharingPlatform.Domain.Entities.Abstract;
using ArtworkSharingPlatform.Domain.Entities.Users;

namespace ArtworkSharingPlatform.Domain.Entities.Artworks;

public class Rating
{
    public int Score { get; set; }
    public int ArtworkId{ get; set; }
    public Artwork? Artwork { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
}