using ArtworkSharingPlatform.Domain.Entities.Artworks;
using ArtworkSharingPlatform.Domain.Helpers;

namespace ArtworkSharingPlatform.Repository.Interfaces
{
	public interface IArtworkRepository
	{
		IQueryable<Artwork> GetArtworksAsQueryable();
		Task UserLike(int userId, int artworkid);
		Task<Artwork> GetArtworkById(int artworkid);
		Task UserComment(int userId, int artworkId, string content);
	}
}
