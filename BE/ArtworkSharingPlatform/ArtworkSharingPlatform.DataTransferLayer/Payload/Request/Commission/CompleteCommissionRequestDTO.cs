using System.Text.Json.Serialization;

namespace ArtworkSharingPlatform.DataTransferLayer.Payload.Request.Commission;

public class CompleteCommissionRequestDTO
{
    [JsonIgnore]
    public int ReceiverId { get;set; }
    public int CommissionRequestId { get; set; }
}