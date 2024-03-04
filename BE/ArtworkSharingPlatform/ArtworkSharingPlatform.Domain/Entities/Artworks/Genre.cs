using ArtworkSharingPlatform.Domain.Entities.Abstract;

namespace ArtworkSharingPlatform.Domain.Entities.Artworks;

public class Genre:BaseEntity
{
    public string? Name { get; set; }
    public Artwork Artwork { get; set; }
}