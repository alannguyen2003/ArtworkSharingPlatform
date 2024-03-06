using ArtworkSharingPlatform.Domain.Common.Enum;

namespace ArtworkSharingPlatform.DataTransferLayer.Payload.Response;

public class CommissionServiceResponse
{
    public CommissionServiceEnum Result { get; set; }
    public string? Message { get; set; }
    public object? ReturnData { get; set; }
}