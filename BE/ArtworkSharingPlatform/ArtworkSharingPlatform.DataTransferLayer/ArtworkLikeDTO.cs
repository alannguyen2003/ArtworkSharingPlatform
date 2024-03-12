using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtworkSharingPlatform.DataTransferLayer
{
    public class ArtworkLikeDTO
    {
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public int ArtworkId { get; set; }
    }
}
