using ArtworkSharingPlatform.Domain.Entities.Artworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtworkSharingPlatform.DataTransferLayer
{
	public class UserProfileDTO
	{
        public string Name { get; set; }
        public string Email { get; set; }
        public string TwitterLink { get; set; }
        public string FacebookLink{ get; set; }
        public string PhoneNumber{ get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public List<ArtworkDTO> Artworks{ get; set; }
        public List<UserProfileFollowDTO> IsFollowedByUsers { get; set; }
    }
}
