namespace ArtworkSharingPlatform.DataTransferLayer.Payload.Response.ConfigManager;

public class ConfigManagerServiceResponse
{
    public string Result { get; set; }
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public object? ReturnData { get; set; }
}