using ArtworkSharingPlatform.Domain.Entities.Abstract;
using ArtworkSharingPlatform.Domain.Entities.Orders;
using ArtworkSharingPlatform.Domain.Entities.Users;

namespace ArtworkSharingPlatform.Domain.Entities.Artworks;

public class Artwork : BaseEntity
{
    private string? _description;
    private decimal _price;
    private int _releaseCount;
    private int _ownerId;
    public User? Owner;
    public ICollection<ArtworkImage>? ArtworkImages;
    public ICollection<Like>? Likes;
    public ICollection<Comment>? Comments;
    public ICollection<Rating>? Ratings;
    public PreOrder? PreOrder;
    public string Description
    {
        get => _description;
        set => _description = value ?? throw new ArgumentNullException(nameof(value));
    }

    public decimal Price
    {
        get => _price;
        set => _price = value;
    }

    public int ReleaseCount
    {
        get => _releaseCount;
        set => _releaseCount = value;
    }

    public int OwnerId
    {
        get => _ownerId;
        set => _ownerId = value;
    }
}