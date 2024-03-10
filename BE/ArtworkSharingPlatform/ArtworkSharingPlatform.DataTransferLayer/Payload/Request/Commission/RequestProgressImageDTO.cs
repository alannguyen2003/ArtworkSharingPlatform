using System.Text.Json.Serialization;

namespace ArtworkSharingPlatform.DataTransferLayer.Payload.Request.Commission;

public class RequestProgressImageDTO
{
    [JsonIgnore]
    public int SenderId { get; set; }
    public int CommissionRequestId { get; set; }
}