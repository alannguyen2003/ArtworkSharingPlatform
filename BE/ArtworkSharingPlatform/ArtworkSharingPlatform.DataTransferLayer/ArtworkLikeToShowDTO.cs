using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtworkSharingPlatform.DataTransferLayer
{
    public class ArtworkLikeToShowDTO
    {
        public int ArtworkId { get; set; }
        public bool IsLiked { get; set; }
    }
}
