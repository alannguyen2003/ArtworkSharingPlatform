namespace ArtworkSharingPlatform.DataTransferLayer.Payload.Request.CommissionRequest;

public class RejectCommissionRequestDTO
{
    public int CommissionRequestId { get; set; }
    public string? NotAcceptedReason { get; set; }
}