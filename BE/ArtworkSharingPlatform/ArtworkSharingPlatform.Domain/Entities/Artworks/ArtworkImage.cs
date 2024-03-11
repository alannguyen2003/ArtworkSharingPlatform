using ArtworkSharingPlatform.Domain.Entities.Abstract;

namespace ArtworkSharingPlatform.Domain.Entities.Artworks;

public class ArtworkImage : BaseEntity
{
    public string? ImageUrl { get; set; }
    public string? PublicId { get; set; }
    public bool? IsThumbnail { get; set; }
    public int ArtworkId { get; set; }
    public Artwork Artwork { get; set; }
}