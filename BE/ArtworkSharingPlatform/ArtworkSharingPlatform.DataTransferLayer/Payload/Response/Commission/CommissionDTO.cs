namespace ArtworkSharingPlatform.DataTransferLayer.Payload.Response.Commission;

public class CommissionDTO
{
    public int Id { get; set; }
    public decimal MinPrice { get; set; }
    public decimal MaxPrice { get; set; }
    public decimal ActualPrice { get; set; }
    public string? RequestDescription { get; set; }
    public string? NotAcceptedReason { get; set; }
    public DateTime RequestDate { get; set; }
    public byte IsProgressStatus { get; set; }
    public string SenderName { get; set; }
    public string ReceiverName { get; set; }
    public string GenreName { get; set; }
    public string CommissionStatus { get; set; }
}