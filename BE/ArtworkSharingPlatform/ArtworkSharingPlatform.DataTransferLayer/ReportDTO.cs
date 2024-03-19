using ArtworkSharingPlatform.Domain.Entities.Artworks;
using ArtworkSharingPlatform.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtworkSharingPlatform.DataTransferLayer
{
    public class ReportDTO
    {
        public int Id { get; set; }
        public int ReporterId { get; set; }
        public int ArtworkId { get; set; }
        public string content { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
