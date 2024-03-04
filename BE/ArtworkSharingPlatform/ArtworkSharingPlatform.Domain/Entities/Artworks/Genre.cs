using ArtworkSharingPlatform.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtworkSharingPlatform.Domain.Entities.Artworks
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; }
    }
}
