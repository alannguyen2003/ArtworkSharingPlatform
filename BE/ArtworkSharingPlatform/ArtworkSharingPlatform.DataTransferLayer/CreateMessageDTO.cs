namespace ArtworkSharingPlatform.DataTransferLayer
{
    public class CreateMessageDTO
    {
        public string RecipientEmail { get; set; }
        public int ArtworkId { get; set; }
        public string Content { get; set; }
    }
}
