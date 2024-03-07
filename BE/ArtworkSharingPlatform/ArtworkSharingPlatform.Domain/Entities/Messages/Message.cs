using ArtworkSharingPlatform.Domain.Entities.Abstract;
using ArtworkSharingPlatform.Domain.Entities.Artworks;
using ArtworkSharingPlatform.Domain.Entities.Users;

namespace ArtworkSharingPlatform.Domain.Entities.Messages
{
    public class Message : BaseEntity
    {
        public int SenderId { get; set; }
        public string SenderEmail{ get; set; }
        public User Sender{ get; set; }
        public int RecipientId { get; set; }
        public string RecipientEmail { get; set; }
        public User Recipient { get; set; }
        public int ArtworkId { get; set; }
        public Artwork Artwork{ get; set; }
        public string Content { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime? MessageSent { get; set; } = DateTime.UtcNow;

    }
}
