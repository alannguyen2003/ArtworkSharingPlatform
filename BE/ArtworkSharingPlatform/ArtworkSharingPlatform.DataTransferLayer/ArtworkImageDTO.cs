namespace ArtworkSharingPlatform.DataTransferLayer
{
	public class ArtworkImageDTO
	{
		public string? ImageUrl { get; set; }
        public string? PublicId { get; set; }
        public bool? IsThumbnail { get; set; }
		public int ArtworkId { get; set; }
	}
}
