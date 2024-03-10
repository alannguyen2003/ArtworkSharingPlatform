using System.Text.Json.Serialization;

namespace ArtworkSharingPlatform.DataTransferLayer.Payload.Request.Commission;

public class CreateCommissionRequestDTO
{
    public decimal MinPrice{get;set;}
    public decimal MaxPrice{get;set;}
    public string? RequestDescription{get;set;}
    [JsonIgnore]
    public int SenderId{get;set;}
    public int ReceiverId{get;set;}
    public int GenreId{get;set;}
}