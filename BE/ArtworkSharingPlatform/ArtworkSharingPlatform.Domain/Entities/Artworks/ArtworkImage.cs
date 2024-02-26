using ArtworkSharingPlatform.Domain.Entities.Abstract;

namespace ArtworkSharingPlatform.Domain.Entities.Artworks;

public class ArtworkImage : BaseEntity
{
    private int _artworkId;
    private string? _imageUrl;
    public Artwork? Artwork;

    public int ArtworkId
    {
        get => _artworkId;
        set => _artworkId = value;
    }

    public string ImageUrl
    {
        get => _imageUrl;
        set => _imageUrl = value ?? throw new ArgumentNullException(nameof(value));
    }
    
}