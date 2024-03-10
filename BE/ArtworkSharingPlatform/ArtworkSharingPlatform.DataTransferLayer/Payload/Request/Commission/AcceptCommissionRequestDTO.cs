using System.Text.Json.Serialization;

namespace ArtworkSharingPlatform.DataTransferLayer.Payload.Request.Commission;

public class AcceptCommissionRequestDTO
{ 
    [JsonIgnore]
    public int ReceiverId { get;set; }
    public int CommissionRequestId { get; set; }
    public decimal ActualPrice { get; set; }
}