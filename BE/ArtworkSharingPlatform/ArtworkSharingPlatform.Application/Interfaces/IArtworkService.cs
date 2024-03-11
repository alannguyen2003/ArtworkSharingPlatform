using ArtworkSharingPlatform.DataTransferLayer;
using ArtworkSharingPlatform.Domain.Entities.Artworks;
using ArtworkSharingPlatform.Domain.Helpers;

namespace ArtworkSharingPlatform.Application.Interfaces
{
	public interface IArtworkService
	{
		Task<PagedList<ArtworkDTO>> GetArtworksAsync(UserParams userParams);
		Task<ArtworkDTO> GetArtworkAsync(int id);
		Task UserLike(ArtworkLikeDTO like);
        Task UserRating(ArtworkRatingDTO rating);
        Task UserFollow(UserFollowDTO follow);
        Task AddArtwork(ArtworkDTO artwork);
        Task DeleteArtwork(int artworkId);
        Task UpdateArtwork(ArtworkDTO artwork);
        Task<IEnumerable<ArtworkLikeToShowDTO>> GetArtworksLike(int userId);

    }
}
