namespace ArtworkSharingPlatform.DataTransferLayer.Payload.Request;

public class CreateCommissionRequestDTO
{
    public decimal MinPrice;
    public decimal MaxPrice;
    public string? RequestDescription;
    public int SenderId;
    public int ReceiverId;
    public int GenreId;
}