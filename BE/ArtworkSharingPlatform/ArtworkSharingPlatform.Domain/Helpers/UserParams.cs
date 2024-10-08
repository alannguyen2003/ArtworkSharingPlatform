﻿namespace ArtworkSharingPlatform.Domain.Helpers
{
	public class UserParams : PaginationParams
	{
		public decimal MinPrice { get; set; } = 0;
		public decimal MaxPrice { get; set; } = 10000000;
		public string? OrderBy { get; set; }
        public string? Search { get; set; }
        public int[]? GenreIds { get; set; }
    }
}
