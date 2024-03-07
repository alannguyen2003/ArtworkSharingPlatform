using ArtworkSharingPlatform.Domain.Entities.Artworks;
using ArtworkSharingPlatform.Domain.Migrations;
using ArtworkSharingPlatform.Repository.Interfaces;

namespace ArtworkSharingPlatform.Repository.Repository;

public class GenreRepository:IGenreRepository
{
    private ArtworkSharingPlatformDbContext _dbContext;

    public GenreRepository(ArtworkSharingPlatformDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public Genre GetById(int id)
    {
        return _dbContext?.Genres?.Where(genre => genre.Id == id).FirstOrDefault()!;
    }
}