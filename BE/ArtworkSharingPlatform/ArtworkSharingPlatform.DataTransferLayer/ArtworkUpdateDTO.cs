using System.ComponentModel.DataAnnotations;

namespace ArtworkSharingPlatform.DataTransferLayer
{
	public class ArtworkUpdateDTO
	{
        [Required]
        public int Id { get; set; }
        [Required]
		public string Title { get; set; }
		public string? Description { get; set; }
		[Required]
		public decimal Price { get; set; }
		[Required]
		public int ReleaseCount { get; set; }
		[Required]
		public int OwnerId { get; set; }
		[Required]
		public DateTime CreatedDate { get; set; }
		[Required]
		public int GenreId { get; set; }
	}
}
