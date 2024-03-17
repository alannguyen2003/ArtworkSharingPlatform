using ArtworkSharingPlatform.Domain.Entities.Artworks;

namespace ArtworkSharingPlatform.Repository.Repository.Interfaces;

public interface IGenreRepository
{
    Genre GetById(int id);
    Task<List<Genre>> GetAll();
}