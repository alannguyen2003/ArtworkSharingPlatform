using System.Text.Json.Serialization;

namespace ArtworkSharingPlatform.DataTransferLayer.Payload.Request.Commission;

public class RespondProgressImageDTO
{
    [JsonIgnore]
    public int ReceiverId { get; set; }
    public int CommissionRequestId { get; set; }
    public List<string> ImageUrls { get; set; }
}