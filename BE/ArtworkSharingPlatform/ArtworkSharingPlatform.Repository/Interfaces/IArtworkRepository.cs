using ArtworkSharingPlatform.Domain.Entities.Artworks;
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

    }
}
