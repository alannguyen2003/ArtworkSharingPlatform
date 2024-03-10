using ArtworkSharingPlatform.Domain.Common.Constants;

namespace ArtworkSharingPlatform.DataTransferLayer.Payload.Response.Commission;

public class CommissionServiceResponseDTO
{
    public string Result { get; set; }
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public object? ReturnData { get; set; }
}