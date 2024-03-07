using ArtworkSharingPlatform.Domain.Entities.Abstract;
using ArtworkSharingPlatform.Domain.Entities.Users;

namespace ArtworkSharingPlatform.Domain.Entities.Artworks;

public class Follow:BaseEntity
{
    public User? Follower { get; set; }
    public User? Artist { get; set; }
}