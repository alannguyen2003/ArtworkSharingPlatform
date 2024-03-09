namespace ArtworkSharingPlatform.DataTransferLayer.Payload.Request.Commission;

public class AcceptCommissionRequestDTO
{
    public int CommissionRequestId { get; set; }
    public decimal ActualPrice { get; set; }
}