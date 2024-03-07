using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtworkSharingPlatform.DataTransferLayer
{
    public class UserFollowDTO
    {
        public int FollowerId { get; set; }
        public int ArtistId { get; set; }
    }
}
