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
        Task AddArtwork(ArtworkToAddDTO artwork);
        Task DeleteArtwork(int artworkId);
        Task UpdateArtwork(ArtworkToAddDTO artwork);
        Task<IEnumerable<ArtworkLikeToShowDTO>> GetArtworksLike(int userId);
        Task ArtworkComment(ArtworkCommentDTO comment);
        Task<IList<ArtworkDTO>> SearchArtworkByTitle(string search);
        Task<IEnumerable<ArtworkDTO>> SearchArtworkByGenre(int genreId);

    }
}
