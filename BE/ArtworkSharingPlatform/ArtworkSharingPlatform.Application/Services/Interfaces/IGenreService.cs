using ArtworkSharingPlatform.Domain.Entities.Artworks;

namespace ArtworkSharingPlatform.Application.Interfaces
{
	public interface IGenreService
	{
		Task<List<Genre>> GetAll();
	}
}
