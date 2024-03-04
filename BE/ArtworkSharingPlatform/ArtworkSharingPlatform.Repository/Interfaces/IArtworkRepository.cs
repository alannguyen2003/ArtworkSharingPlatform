using ArtworkSharingPlatform.Domain.Entities.Artworks;
using ArtworkSharingPlatform.Domain.Helpers;

namespace ArtworkSharingPlatform.Repository.Interfaces
{
	public interface IArtworkRepository
	{
		IQueryable<Artwork> GetArtworksAsQueryable();
	}
}
