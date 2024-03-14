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
        Task UserFollow(int soureUserId, string email);
        Task AddArtwork(ArtworkToAddDTO artwork);
        Task DeleteArtwork(int artworkId);
        Task UpdateArtwork(ArtworkUpdateDTO artwork);
        Task<IEnumerable<ArtworkLikeToShowDTO>> GetArtworksLike(int userId);
        Task ArtworkComment(ArtworkCommentDTO comment);
        Task<IList<ArtworkDTO>> SearchArtworkByTitle(string search);
        Task<IList<ArtworkDTO>> GetArtistArtwork(int artistId);
        Task<IEnumerable<ArtworkDTO>> SearchArtworkByGenre(int genreId);
        Task<bool> ConfirmSell(int artworkId, int userId);
        Task<bool> SetThumbnail(int id);
        Task<bool> DeleteArtworkImage(ArtworkImageDTO imageDTO);
        Task<ArtworkImage> AddImageToArtwork(ArtworkImageDTO artworkImageDTO);

    }
}
