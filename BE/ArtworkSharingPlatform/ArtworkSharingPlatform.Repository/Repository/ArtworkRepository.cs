using ArtworkSharingPlatform.Domain.Entities.Artworks;
using ArtworkSharingPlatform.Domain.Entities.Users;
using ArtworkSharingPlatform.Domain.Migrations;
using ArtworkSharingPlatform.Repository.Interfaces;
using ArtworkSharingPlatform.Repository.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            var user = _userRepository.GetById(artwork.OwnerId);
            if (user != null)
            {
                if (user.RemainingCredit <= 0) throw new Exception("Insuficient credit");
                user.RemainingCredit -= 1;
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
				index.Status = 1;

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
            return await _context.Artworks.Include(a => a.ArtworkImages).Include(a => a.Owner).ToListAsync();
        }
        public async Task<IEnumerable<Artwork>?> SearchArtworkByGenre(int genreId)
        {

            var result = await _context.Artworks.Where(x => x.GenreId ==  genreId).ToListAsync();
            return result;

        }
        public async Task AddArtworkImage(ArtworkImage artwork)
        {
                if (artwork != null)
                {
                    _context.ArtworkImages.AddAsync(artwork);

                    await _context.SaveChangesAsync();
                }
        }
        public async Task UpdateArtworkImage(ArtworkImage artwork)
        {
                if (artwork != null)
                {
                    var index = await _context.ArtworkImages.Where(x => x.ArtworkId == artwork.ArtworkId).FirstOrDefaultAsync();

                    if (index != null)
                    {
                        index.ImageUrl = artwork.ImageUrl;
                        index.PublicId = artwork.PublicId;
                        index.IsThumbnail = artwork.IsThumbnail;
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        throw new KeyNotFoundException("An ArtworkImage with the specified ID could not be found.");
                    }
                }
        }

        public async Task ArtworkReport (Report report)
        {
            if (report != null)
            {
                report.Status = "Pending";
                await _context.Reports.AddAsync(report);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ConfirmSell(int artworkId, int userId, string buyUserEmail)
        {
            var flag = false;
            var artwork = await _context.Artworks.FindAsync(artworkId);
            if (artwork.OwnerId != userId)
            {
                return flag;
            }
            if (artwork != null)
            {
                artwork.Status = 0;
            }
            var buyUser = await _context.Users.Where(x => x.Email == buyUserEmail).SingleOrDefaultAsync();
            var purchase = new Purchase
            {
                BuyUserId = buyUser.Id,
                SellUserId = userId,
                ArtworkId = artwork.Id,
                BuyPrice = artwork.Price,
                BuyDate = DateTime.UtcNow
            };
            _context.Purchases.Add(purchase);
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

		public async Task<int> GetArtworkRatingForUser(int userId, int artworkId)
		{
            var rating = await _context.Ratings.SingleOrDefaultAsync(x => x.UserId == userId && x.ArtworkId == artworkId);
            if(rating == null)  return 0; 
			return rating.Score;
		}
	
        public async Task<IEnumerable<Comment>> ListArtworkComments(int artworkId)
        {
            if (artworkId == null)
            {
                return null;
            }
            
            return await _context.Comments.Where(x => x.ArtworkId == artworkId).ToListAsync();
        }

        public async Task<IEnumerable<Purchase>> ListBoughtArtwork(int sellUserId)
        {
            if (sellUserId == null)
            {
                return null;
            }

            return await _context.Purchases.Where(x => x.SellUserId == sellUserId).ToListAsync();
        }
        public async Task<IEnumerable<Purchase>> ListSoldArtwork(int soldUserId)
        {
            if (soldUserId == null)
            {
                return null;
            }

            return await _context.Purchases.Where(x => x.SellUserId == soldUserId).ToListAsync();
        }
        public async Task AddPurchase(Purchase purchase)
        {
            var artwork = await _context.Artworks.Where(x => x.Id == purchase.ArtworkId)
                .FirstOrDefaultAsync();
            
            if (purchase == null)
            {
                return ;
            }
            if (artwork == null)
            {
                return;
            }

            purchase.BuyPrice = artwork.Price;
            await _context.Purchases.AddAsync(purchase);
            await _context.SaveChangesAsync();
            
            
            artwork.Status = 0;
            await _context.SaveChangesAsync();
        }
        public async Task<bool> CreditAvailable (int userId)
        {
            if (userId == 0 && userId == null) { return false; }
            var user = await _context.Users.Where(x => x.Id == userId)
                .FirstOrDefaultAsync();
            if (user.RemainingCredit >= 1)
            {
                user.RemainingCredit -= 1;
                return true;
            }
            return false;
        }
        public async Task ActiveArtworkStatus(int artworkId, int userId)
        {
            if (artworkId == null && artworkId ==0 && userId==0)
            {
                return;
            }

            var user = await _context.Users.Where(x => x.Id == userId)
                .FirstOrDefaultAsync();
            var artwork = await _context.Artworks.Where(x => x.Id == artworkId)
                .FirstOrDefaultAsync();
            if (user == null && artwork == null) { return; }
                artwork.Status = 1;
                await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Purchase>> ListHistoryPurchaseArtwork (int artworkId)
        {
            if (artworkId == null && artworkId == 0)
            {
                return null;
            }

            return await _context.Purchases.Where(x => x.ArtworkId == artworkId).Include(x => x.Artwork).ToListAsync();
        }

		public async Task<bool> CheckArtworkAvailability(int artworkId)
		{
            var artwork = await _context.Artworks.FindAsync(artworkId);
            return artwork.Status == 1;
		}
	}
}
