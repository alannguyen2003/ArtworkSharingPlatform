using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtworkSharingPlatform.DataTransferLayer
{
    public class ArtworkUpdateDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int ReleaseCount { get; set; }
        public int GenreId { get; set; }
        public int OwnerId { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public byte Status { get; set; }
    }
}
