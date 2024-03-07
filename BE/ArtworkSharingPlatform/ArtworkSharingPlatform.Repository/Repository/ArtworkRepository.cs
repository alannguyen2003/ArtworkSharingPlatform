using ArtworkSharingPlatform.Domain.Entities.Artworks;
using ArtworkSharingPlatform.Domain.Helpers;
using ArtworkSharingPlatform.Domain.Migrations;
using ArtworkSharingPlatform.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Reflection.Metadata.Ecma335;

namespace ArtworkSharingPlatform.Repository.Repository
{
    public class ArtworkRepository : IArtworkRepository
    {
        private readonly ArtworkSharingPlatformDbContext _context;
        private readonly IUserRepository _userRepository;

        public ArtworkRepository(ArtworkSharingPlatformDbContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

		public IQueryable<Artwork> GetArtworksAsQueryable()
        {
            return _context.Artworks.AsQueryable();
        }
        public async Task<Artwork> GetArtworkById (int artworkid)
        {
            return await _context.Artworks.FirstOrDefaultAsync(a => a.Id.Equals(artworkid));
        }
        public async Task UserLike(int userId, int artworkid)
        {
            Like like = new Like
            {
                Artwork = await GetArtworkById(artworkid),
                User = await _userRepository.GetUserById(userId)
            };
            //var index = await _context.Likes.Where(a => a.Artwork.Equals(like.Artwork) && a.User.Equals(like.User)).FirstOrDefaultAsync();
            var isLike = await _context.Likes.AnyAsync(a => a.Artwork.Equals(like.Artwork) && a.User.Equals(like.User));
            if (like != null && !isLike)
            {
                _context.Likes.Remove(like);
                await _context.SaveChangesAsync();

            } else if (like != null)
            {
                _context.Likes.AddAsync(like);
                await _context.SaveChangesAsync();
            }
        }

        /*public async Task UserFollow(int userId, int artworkid)
        {
            Like like = new Like
            {
                Artwork = await GetArtworkById(artworkid),
                User = await _userRepository.GetUserById(userId)
            };
            //var index = await _context.Likes.Where(a => a.Artwork.Equals(like.Artwork) && a.User.Equals(like.User)).FirstOrDefaultAsync();
            var isLike = await _context.Likes.AnyAsync(a => a.Artwork.Equals(like.Artwork) && a.User.Equals(like.User));
            if (like != null && !isLike)
            {
                _context.Likes.Remove(like);
                await _context.SaveChangesAsync();
            }
            else if (like != null)
            {
                _context.Likes.AddAsync(like);
                await _context.SaveChangesAsync();
            }
        }*/
        public async Task UserRating(int userId, int artworkid, int score)
        {
            Rating rate = new Rating
            {
                Artwork = await GetArtworkById(artworkid),
                User = await _userRepository.GetUserById(userId),
                Score = score
            };
            //var index = await _context.Likes.Where(a => a.Artwork.Equals(like.Artwork) && a.User.Equals(like.User)).FirstOrDefaultAsync();
            var isRate = await _context.Ratings.FirstOrDefaultAsync(a => a.Artwork.Equals(rate.Artwork) && a.User.Equals(rate.User));
            if (rate != null && isRate != null)
            {
                
                await _context.SaveChangesAsync();

            }
            else if (rate != null)
            {
                _context.Ratings.AddAsync(rate);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UserComment (int userId, int artworkId, string content)
        {
            Comment comment = new Comment
            {
                Content = content,
                User = await _userRepository.GetUserById(userId),
                Artwork = await GetArtworkById(artworkId)
            };
            if (comment != null)
            {
                _context.Comments.AddAsync(comment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
