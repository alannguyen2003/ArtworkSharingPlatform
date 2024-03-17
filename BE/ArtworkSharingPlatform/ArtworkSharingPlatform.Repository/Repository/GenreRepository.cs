using ArtworkSharingPlatform.Domain.Entities.Artworks;
using ArtworkSharingPlatform.Domain.Migrations;
using ArtworkSharingPlatform.Repository.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ArtworkSharingPlatform.Repository.Repository;

public class GenreRepository:IGenreRepository
{
    private readonly ArtworkSharingPlatformDbContext _dbContext;

    public GenreRepository(ArtworkSharingPlatformDbContext dbContext)
    {
        _dbContext = dbContext;
    }

	public async Task<List<Genre>> GetAll()
	{
        return await _dbContext.Genres.ToListAsync();
	}

	public Genre GetById(int id)
    {
        return _dbContext?.Genres?.Where(genre => genre.Id == id).FirstOrDefault()!;
    }
}