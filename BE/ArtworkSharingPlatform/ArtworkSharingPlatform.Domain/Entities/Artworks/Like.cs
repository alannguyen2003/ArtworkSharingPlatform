using ArtworkSharingPlatform.Domain.Entities.Abstract;
using ArtworkSharingPlatform.Domain.Entities.Users;

namespace ArtworkSharingPlatform.Domain.Entities.Artworks;

public class Like : BaseEntity
{
    private int _userId;
    public User? User;
    public Artwork? Artwork;

    public int UserId
    {
        get => _userId;
        set => _userId = value;
    }
}