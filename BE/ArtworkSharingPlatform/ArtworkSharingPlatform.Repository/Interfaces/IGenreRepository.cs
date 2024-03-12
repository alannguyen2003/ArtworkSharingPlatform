using ArtworkSharingPlatform.Domain.Entities.Artworks;

namespace ArtworkSharingPlatform.Repository.Interfaces;

public interface IGenreRepository
{
    Genre GetById(int id);
    Task<List<Genre>> GetAll();
}