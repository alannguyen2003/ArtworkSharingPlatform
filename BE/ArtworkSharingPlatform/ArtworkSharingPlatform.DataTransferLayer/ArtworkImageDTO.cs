namespace ArtworkSharingPlatform.DataTransferLayer
{
	public class ArtworkImageDTO
	{
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
        public string? PublicId { get; set; }
        public bool? IsThumbnail { get; set; }
		public int ArtworkId { get; set; }
	}
}
