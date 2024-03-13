using ArtworkSharingPlatform.Domain.Entities.Abstract;
using ArtworkSharingPlatform.Domain.Entities.Users;

namespace ArtworkSharingPlatform.Domain.Entities.Artworks;

public class Follow
{
    public User SourceUser { get; set; }
    public int SourceUserId { get; set; }
    public User TargetUser { get; set; }
    public int TargetUserId { get; set; }
}