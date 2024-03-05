namespace ArtworkSharingPlatform.DataTransferLayer
{
	public class ArtworkDTO
	{
        public int Id { get; set; }
        public string Title { get; set; }
		public string? Description { get; set; }
		public decimal Price { get; set; }
		public int ReleaseCount { get; set; }
		public int OwnerId { get; set; }
		public string ImageUrl{ get; set; }
		public DateTime CreatedDate { get; set; }
		public byte Status { get; set; }
        public List<ArtworkImageDTO> ArtworkImages { get; set; }
        public ArtworkUserDTO User { get; set; }
    }
}
