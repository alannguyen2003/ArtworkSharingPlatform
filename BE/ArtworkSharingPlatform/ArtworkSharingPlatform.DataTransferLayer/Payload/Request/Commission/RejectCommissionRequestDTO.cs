namespace ArtworkSharingPlatform.DataTransferLayer.Payload.Request.Commission;

public class RejectCommissionRequestDTO
{
    public int CommissionRequestId { get; set; }
    public string? NotAcceptedReason { get; set; }
}