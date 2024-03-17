using ArtworkSharingPlatform.Domain.Entities.Users;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtworkSharingPlatform.Domain.Entities.Artworks
{
    public class Purchase
    {
        public int BuyUserId { get; set; }
        public User BuyUser { get; set; }
        public int SellUserId { get; set; }
        public User SellUser { get; set; }
        public int ArtworkId { get; set; }
        public Artwork Artwork { get; set; }
        public DateTime BuyDate { get; set; }
        public decimal BuyPrice { get; set; }
    }
}
