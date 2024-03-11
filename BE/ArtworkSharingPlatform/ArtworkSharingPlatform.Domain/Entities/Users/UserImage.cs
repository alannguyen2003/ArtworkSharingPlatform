using ArtworkSharingPlatform.Domain.Entities.Abstract;

namespace ArtworkSharingPlatform.Domain.Entities.Users
{
	public class UserImage : BaseEntity
	{
        public string Url { get; set; }
        public string? PublicId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
