using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtworkSharingPlatform.DataTransferLayer
{
    public class UserFollowDTO
    {
        [Required]
        public int TargetUserId { get; set; }
        [Required]
        public int SourceUserId { get; set; }
    }
}
