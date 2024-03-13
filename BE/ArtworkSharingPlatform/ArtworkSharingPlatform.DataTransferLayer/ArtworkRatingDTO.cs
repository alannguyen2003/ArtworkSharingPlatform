using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtworkSharingPlatform.DataTransferLayer
{
    public class ArtworkRatingDTO
    {
        public int UserId { get; set; }
        public int ArtworkId { get; set; }
        public int Score { get; set; }
    }
}
