using ArtworkSharingPlatform.Domain.Entities.Artworks;
using ArtworkSharingPlatform.Domain.Helpers;
using ArtworkSharingPlatform.Domain.Migrations;
using ArtworkSharingPlatform.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        public async Task UserLike(Like like)
        {
                var isLike = await _context.Likes.AnyAsync(a => a.Artwork.Equals(like.Artwork) && a.User.Equals(like.User));
                if (like != null && !isLike)
                {
                    var index = await _context.Likes.Where(a => a.Artwork.Equals(like.Artwork) && a.User.Equals(like.User)).FirstOrDefaultAsync();
                    _context.Likes.Remove(index);
                    await _context.SaveChangesAsync();

                }
                else if (like != null)
                {
                    _context.Likes.AddAsync(like);
                    await _context.SaveChangesAsync();
                }
        }

        public async Task UserFollow(Follow follow)
        {
                //var index = await _context.Likes.Where(a => a.Artwork.Equals(like.Artwork) && a.User.Equals(like.User)).FirstOrDefaultAsync();
                var isFollow = await _context.Follows.AnyAsync(a => a.Artist.Equals(follow.Artist) && a.Follower.Equals(follow.Follower));
                if (follow != null && !isFollow)
                {
                    var index = await _context.Follows.FirstOrDefaultAsync(a => a.Artist.Equals(follow.Artist) && a.Follower.Equals(follow.Follower));
                    _context.Follows.Remove(index);
                    await _context.SaveChangesAsync();
                }
                else if (follow != null)
                {
                    _context.Follows.AddAsync(follow);
                    await _context.SaveChangesAsync();
                }
        }
        public async Task UserRating(Rating rate)
        {                
                var isRate = await _context.Ratings.FirstOrDefaultAsync(a => a.Artwork.Equals(rate.Artwork) && a.User.Equals(rate.User));
                if (rate != null && isRate != null)
                {
                    var index = await _context.Ratings.Where(a => a.Artwork.Equals(rate.Artwork) && a.User.Equals(rate.User)).FirstOrDefaultAsync();
                    _context.Entry(index).CurrentValues.SetValues(rate);

                    await _context.SaveChangesAsync();
                }
                else if (rate != null)
                {
                    _context.Ratings.AddAsync(rate);
                    await _context.SaveChangesAsync();
                }

        }

        public async Task UserComment (Comment comment)
        {
                
                if (comment != null)
                {
                    _context.Comments.AddAsync(comment);
                    await _context.SaveChangesAsync();
                }

        }

        public async Task<IEnumerable<Artwork>?> SearchArtwork(string search)
        {

                var result = _context.Artworks.Where(x => x.Title.Equals(search.ToLower())).ToList();
                return result;

        }

        public async Task AddArtwork(Artwork artwork)
        {

                if (artwork != null)
                {
                    _context.Artworks.AddAsync(artwork);

                    await _context.SaveChangesAsync();
                }
            

        }

        public async Task DeleteArtwork(int artworkId)
        {
                if (artworkId != null)
                {
                    var index = await _context.Artworks.FindAsync(artworkId);
                    index.Status = 0;

                    _context.Entry(index).CurrentValues.SetValues(index);

                    await _context.SaveChangesAsync();
                }

        }

        public async Task UpdateArtwork(Artwork artwork)
        {
                if (artwork != null)
                {
                    var index = await _context.Artworks.FindAsync(artwork.Id);

                    _context.Entry(index).CurrentValues.SetValues(artwork);

                    await _context.SaveChangesAsync();
                }
            
        }

        public async Task<bool> HasUserLikedArtwork(int userId, int artworkId)
        {
                return await _context.Likes.AnyAsync(a => a.User.Id == userId && a.Artwork.Id == artworkId);
        }

        public async Task<IEnumerable<Artwork>> GetArtworksAsync()
        {
            return await _context.Artworks.ToListAsync();
        }

    }
}
