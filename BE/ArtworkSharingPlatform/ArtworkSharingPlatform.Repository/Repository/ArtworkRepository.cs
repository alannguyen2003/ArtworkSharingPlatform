using ArtworkSharingPlatform.Domain.Entities.Artworks;
using ArtworkSharingPlatform.Domain.Helpers;
using ArtworkSharingPlatform.Domain.Migrations;
using ArtworkSharingPlatform.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.ComponentModel;
using System.Linq;
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
                var isLike = await _context.Likes.AnyAsync(a => a.ArtworkId == like.ArtworkId && a.UserId == like.UserId);
                if (like != null && isLike)
                {
                    var index = await _context.Likes.Where(a => a.ArtworkId == like.ArtworkId && a.UserId == like.UserId).FirstOrDefaultAsync();
                    _context.Likes.Remove(index);
                    await _context.SaveChangesAsync();

                }
                else if (like != null)
                {
                    await _context.Likes.AddAsync(like);
                    await _context.SaveChangesAsync();
                }
        }

        public async Task UserFollow(Follow follow)
        {
                var isFollow = await _context.Follows.AnyAsync(a => a.TargetUserId == follow.TargetUserId && a.SourceUserId == follow.SourceUserId);
                if (follow != null && isFollow)
                {
                    var index = await _context.Follows.Where(a => a.TargetUserId == follow.TargetUserId && a.SourceUserId == follow.SourceUserId).FirstOrDefaultAsync();
                    _context.Follows.Remove(index);
                    await _context.SaveChangesAsync();
                }
                else if (follow != null)
                {
                    await _context.Follows.AddAsync(follow);
                    await _context.SaveChangesAsync();
                }
        }
        public async Task UserRating(Rating rate)
        {                
                var isRate = await _context.Ratings.AnyAsync(a => a.ArtworkId == rate.ArtworkId && a.UserId == rate.UserId);
                if (rate != null && isRate)
                {
                    var index = await _context.Ratings.Where(a => a.ArtworkId == rate.ArtworkId && a.UserId == rate.UserId).FirstOrDefaultAsync();
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

        public async Task<IList<Artwork>?> SearchArtwork(string search)
        {

            IList<Artwork> artworks = new List<Artwork>();
                artworks = await _context.Artworks
                    .Include(a => a.Genre)
                    .Include(a => a.Owner)
                    .Include(a => a.ArtworkImages).Where(a => a.Title.Contains(search))
                    .ToListAsync();
            return artworks;

        }

        public async Task AddArtwork(Artwork artwork)
        {

                if (artwork != null)
                {
                    artwork.Status = 1;
                    await _context.Artworks.AddAsync(artwork);

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
        public async Task<IEnumerable<Artwork>?> SearchArtworkByGenre(int genreId)
        {

            var result = await _context.Artworks.Where(x => x.GenreId ==  genreId).ToListAsync();
            return result;

        }

        public async Task<bool> ConfirmSell(int artworkId, int userId)
        {
            var flag = false;
            var artwork = await _context.Artworks.FindAsync(artworkId);
            if (artwork.OwnerId != userId)
            {
                return flag;
            }
            if (artwork != null)
            {
                if(artwork.ReleaseCount > 0)
                {
					artwork.ReleaseCount--;
				}
            }
            flag = await _context.SaveChangesAsync() > 0;
            return flag;
        }

		public async Task<bool> SetThumbNail(int id)
		{
            var flag = false;
            var image = await _context.ArtworkImages.FindAsync(id);
            if (image == null)
            {
                return flag;
            }
            var thumbNailImage = await _context.ArtworkImages.SingleOrDefaultAsync(x => x.ArtworkId == image.ArtworkId && x.Id != id && x.IsThumbnail.Value);
            if (thumbNailImage != null)
            {
                thumbNailImage.IsThumbnail = false;
            }
            image.IsThumbnail = true;
            flag = await _context.SaveChangesAsync() > 0;
            return flag;
		}
		public async Task<bool> DeleteArtworkImage(ArtworkImage image)
		{
			var flag = false;
			if (image == null)
			{
				return flag;
			}
			if (image.IsThumbnail.Value)
			{
                return false;
			}
            _context.ArtworkImages.Remove(image);
			flag = await _context.SaveChangesAsync() > 0;
			return flag;
		}
		public async Task<ArtworkImage> AddImageToArtwork(ArtworkImage artworkImage)
		{
            if (artworkImage == null)
            {
                return null;
            }
            await _context.ArtworkImages.AddAsync(artworkImage);
			await _context.SaveChangesAsync();
			return artworkImage;
		}
	}
}
