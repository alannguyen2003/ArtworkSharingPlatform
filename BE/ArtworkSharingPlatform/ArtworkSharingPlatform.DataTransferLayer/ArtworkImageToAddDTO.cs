using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtworkSharingPlatform.DataTransferLayer
{
    public class ArtworkImageToAddDTO
    {
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public string PublicId { get; set; }
        [Required]
        public bool IsThumbnail { get; set; }
        public int ArtworkId { get; set; }
    }
}
