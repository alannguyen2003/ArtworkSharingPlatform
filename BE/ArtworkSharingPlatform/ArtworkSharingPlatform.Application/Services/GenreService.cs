using ArtworkSharingPlatform.Application.Interfaces;
using ArtworkSharingPlatform.Domain.Entities.Artworks;
using ArtworkSharingPlatform.Repository.Repository.Interfaces;

namespace ArtworkSharingPlatform.Application.Services
{
	public class GenreService : IGenreService
	{
		private readonly IGenreRepository _genreRepository;

		public GenreService(IGenreRepository genreRepository)
        {
			_genreRepository = genreRepository;
		}
        public async Task<List<Genre>> GetAll()
		{
			return await _genreRepository.GetAll();
		}
	}
}
