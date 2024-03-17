namespace ArtworkSharingPlatform.DataTransferLayer.Payload.Request.Artwork;

public class ArtworkByGenreRequestDTO
{
    public int GenreId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize{ get; set; }
}