using ArtworkSharingPlatform.Domain.Entities.Abstract;
using ArtworkSharingPlatform.Domain.Entities.Users;

namespace ArtworkSharingPlatform.Domain.Entities.Artworks;

public class Rating : BaseEntity
{
    private int _userId;
    private int _score;
    public User? User;
    public Artwork? Artwork;

    public int UserId
    {
        get => _userId;
        set => _userId = value;
    }

    public int Score
    {
        get => _score;
        set => _score = value;
    }
}