using ArtworkSharingPlatform.Domain.Entities.Abstract;
using ArtworkSharingPlatform.Domain.Entities.Users;

namespace ArtworkSharingPlatform.Domain.Entities.Artworks;

public class Comment : BaseEntity
{
    private int _userId;
    private string? _content;
    public User? User;
    public Artwork? Artwork;

    public int UserId
    {
        get => _userId;
        set => _userId = value;
    }

    public string? Content
    {
        get => _content;
        set => _content = value;
    }
}