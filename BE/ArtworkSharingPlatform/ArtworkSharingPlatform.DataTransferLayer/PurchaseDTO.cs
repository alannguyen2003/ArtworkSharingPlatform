using ArtworkSharingPlatform.Domain.Entities.Artworks;
using ArtworkSharingPlatform.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtworkSharingPlatform.DataTransferLayer
{
    public class PurchaseDTO
    {
        public int BuyUserId { get; set; }
        public int SellUserId { get; set; }
        public int ArtworkId { get; set; }
        public DateTime BuyDate { get; set; }
        public decimal BuyPrice { get; set; }
    }
}
