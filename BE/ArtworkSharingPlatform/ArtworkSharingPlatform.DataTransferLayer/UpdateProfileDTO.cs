using System.ComponentModel.DataAnnotations;

namespace ArtworkSharingPlatform.DataTransferLayer
{
	public class UpdateProfileDTO
	{
		[Required]
		public string Name { get; set; }
		[Required]
		public string PhoneNumber { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string Description { get; set; }
        public string? TwitterLink { get; set; }
        public string? FacebookLink { get; set; }
    }
}
