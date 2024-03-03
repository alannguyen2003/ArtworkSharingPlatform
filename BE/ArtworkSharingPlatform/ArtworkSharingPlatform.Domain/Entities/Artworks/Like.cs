using ArtworkSharingPlatform.Domain.Entities.Abstract;
using ArtworkSharingPlatform.Domain.Entities.Users;

namespace ArtworkSharingPlatform.Domain.Entities.Artworks;

public class Like : BaseEntity
{
    public User? User { get; set; }
    public Artwork? Artwork { get; set; }
}