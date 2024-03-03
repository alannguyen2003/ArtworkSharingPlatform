using ArtworkSharingPlatform.Domain.Entities.Abstract;
using ArtworkSharingPlatform.Domain.Entities.Users;

namespace ArtworkSharingPlatform.Domain.Entities.Artworks;

public class Rating : BaseEntity
{
    public int Score { get; set; }
    public Artwork? Artwork { get; set; }
}