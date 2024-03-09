namespace ArtworkSharingPlatform.DataTransferLayer.Payload.Request.Commission;

public class RespondProgressImageDTO
{
    public int ReceiverId { get; set; }
    public int CommissionRequestId { get; set; }
    public List<string> ImageUrls { get; set; }
}