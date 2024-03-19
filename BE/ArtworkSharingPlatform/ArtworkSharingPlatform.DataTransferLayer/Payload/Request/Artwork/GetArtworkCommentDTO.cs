namespace ArtworkSharingPlatform.DataTransferLayer.Payload.Request.Artwork;

public class GetArtworkCommentDTO:ArtworkCommentDTO
{
    public string UserName { get; set; }
    public string AvatarUrl { get; set; }
}