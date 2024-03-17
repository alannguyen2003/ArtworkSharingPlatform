namespace ArtworkSharingPlatform.DataTransferLayer
{
	public class UserImageDTO
	{
        public string Url { get; set; }
        public string? PublicId { get; set; }
        public int? UserId { get; set; }
    }
}
