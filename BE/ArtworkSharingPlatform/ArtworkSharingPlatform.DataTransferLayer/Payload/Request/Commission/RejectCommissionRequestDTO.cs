using System.Text.Json.Serialization;

namespace ArtworkSharingPlatform.DataTransferLayer.Payload.Request.Commission;

public class RejectCommissionRequestDTO
{
    [JsonIgnore]
    public int ReceiverId { get; set; }
    public int CommissionRequestId { get; set; }
    public string? NotAcceptedReason { get; set; }
}