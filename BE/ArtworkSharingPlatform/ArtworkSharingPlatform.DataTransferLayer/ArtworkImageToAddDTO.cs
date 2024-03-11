using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtworkSharingPlatform.DataTransferLayer
{
    public class ArtworkImageToAddDTO
    {
        public string? ImageUrl { get; set; }
        public string? PublicId { get; set; }
        public bool? IsThumbnail { get; set; }
        public int ArtworkId { get; set; }
    }
}
