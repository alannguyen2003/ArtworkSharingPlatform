using ArtworkSharingPlatform.Domain.Entities.Abstract;

namespace ArtworkSharingPlatform.Domain.Entities.Artworks;

public class ArtworkImage : BaseEntity
{
    public string? ImageUrl { get; set; }
    public Artwork Artwork { get; set; }
}