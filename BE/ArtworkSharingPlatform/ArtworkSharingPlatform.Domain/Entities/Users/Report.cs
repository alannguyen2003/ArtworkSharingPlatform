using ArtworkSharingPlatform.Domain.Entities.Abstract;
using ArtworkSharingPlatform.Domain.Entities.Artworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtworkSharingPlatform.Domain.Entities.Users
{
    public class Report : BaseEntity
    {
        public int ReporterId { get; set; }
        public User Reporter { get; set; }
        public int ArtworkId { get; set; }
        public Artwork Artwork { get; set; }
        public string content { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }
    }
}
