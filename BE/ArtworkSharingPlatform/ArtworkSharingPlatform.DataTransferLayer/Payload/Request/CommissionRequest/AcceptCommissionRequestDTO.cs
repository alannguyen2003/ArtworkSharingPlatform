namespace ArtworkSharingPlatform.DataTransferLayer.Payload.Request.CommissionRequest;

public class AcceptCommissionRequestDTO
{
    public int CommissionRequestId { get; set; }
    public decimal ActualPrice { get; set; }
}