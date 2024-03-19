using ArtworkSharingPlatform.Domain.Entities.Artworks;
using ArtworkSharingPlatform.Domain.Entities.Users;
using ArtworkSharingPlatform.Domain.Helpers;

namespace ArtworkSharingPlatform.Repository.Interfaces
{
	public interface IArtworkRepository
	{
		IQueryable<Artwork> GetArtworksAsQueryable();
		Task UserLike(Like like);
		Task<Artwork> GetArtworkById(int artworkid);
		Task UserComment(Comment comment);
		Task UserRating(Rating rating);
		Task UserFollow(Follow follow);
		Task AddArtwork(Artwork artwork);
		Task DeleteArtwork(int artworkId);
		Task UpdateArtwork(Artwork artwork);
		Task<bool> HasUserLikedArtwork(int userId, int artworkId);
		Task<IEnumerable<Artwork>> GetArtworksAsync();
		Task<IList<Artwork>?> SearchArtwork(string search);
        Task<IEnumerable<Artwork>?> SearchArtworkByGenre(int genreId);
		Task AddArtworkImage(ArtworkImage artwork);
		Task UpdateArtworkImage(ArtworkImage artwork);
		Task ArtworkReport(Report report);
        Task<bool> ConfirmSell(int artworkId, int userId, string buyerEmail);
        Task<bool> DeleteArtworkImage(ArtworkImage image);
		Task<bool> SetThumbNail(int id);
		Task<ArtworkImage> AddImageToArtwork(ArtworkImage artworkImage);
		Task<int> GetArtworkRatingForUser(int userId, int artworkId);
		Task<IEnumerable<Comment>> ListArtworkComments(int artworkId);
		Task ActiveArtworkStatus(int artworkId, int userId);
		Task<bool> CreditAvailable(int userId);
		Task<bool> CheckArtworkAvailability(int artworkId);

    }
}
