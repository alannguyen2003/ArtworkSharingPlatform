using ArtworkSharingPlatform.Domain.Entities.Artworks;
using ArtworkSharingPlatform.Domain.Helpers;
using ArtworkSharingPlatform.Domain.Migrations;
using ArtworkSharingPlatform.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ArtworkSharingPlatform.Repository.Repository
{
    public class ArtworkRepository : IArtworkRepository
    {
        private readonly ArtworkSharingPlatformDbContext _context;

        public ArtworkRepository(ArtworkSharingPlatformDbContext context)
        {
            _context = context;
        }

		public IQueryable<Artwork> GetArtworksAsQueryable()
        {
            return _context.Artworks.AsQueryable();
        }
    }
}
