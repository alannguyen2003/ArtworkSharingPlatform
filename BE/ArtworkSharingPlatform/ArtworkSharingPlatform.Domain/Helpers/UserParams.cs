namespace ArtworkSharingPlatform.Domain.Helpers
{
	public class UserParams : PaginationParams
	{
		public int CurrentUserId { get; set; }
		public decimal MinPrice { get; set; } = 0;
		public decimal MaxPrice { get; set; } = 10000000;
		public string? OrderBy { get; set; }
	}
}
