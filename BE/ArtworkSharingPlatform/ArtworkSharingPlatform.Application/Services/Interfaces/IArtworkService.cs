using ArtworkSharingPlatform.DataTransferLayer;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Response;
using ArtworkSharingPlatform.Domain.Entities.Artworks;
using ArtworkSharingPlatform.Domain.Helpers;

namespace ArtworkSharingPlatform.Application.Interfaces
{
    public interface IArtworkService
    {
        Task<PagedList<ArtworkDTO>> GetArtworksAsync(UserParams userParams);
        Task<List<ArtworkDTO>> GetArtworksByGenre(int genreId);
        Task<ArtworkDTO> GetArtworkAsync(int id);
        Task UserLike(ArtworkLikeDTO like);
        Task UserRating(ArtworkRatingDTO rating);
        Task UserFollow(int soureUserId, string email);
        Task AddArtwork(ArtworkToAddDTO artwork);
        Task DeleteArtwork(int artworkId);
        Task UpdateArtwork(ArtworkUpdateDTO artwork);
        Task<IEnumerable<ArtworkLikeToShowDTO>> GetArtworksLike(int userId);
        Task<List<ArtworkAdminDTO>> GetArtworkAdmin();
        Task ArtworkComment(ArtworkCommentDTO comment);
        Task<IList<ArtworkDTO>> SearchArtworkByTitle(string search);
        Task<IList<ArtworkDTO>> GetArtistArtwork(int artistId);
        Task<IEnumerable<ArtworkDTO>> SearchArtworkByGenre(int genreId);
        Task<bool> ConfirmSell(int artworkId, int userId);
        Task<bool> SetThumbnail(int id);
        Task<bool> DeleteArtworkImage(ArtworkImageDTO imageDTO);
        Task<ArtworkImage> AddImageToArtwork(ArtworkImageDTO artworkImageDTO);
        Task AddArtworkImage(ArtworkImageToAddDTO _artwork);
        Task UpdateArtworkImage(ArtworkImageToAddDTO _artwork);
        Task ReportArtwork(ReportDTO _report);
        Task<int> GetArtworkRatingForUser(int userId, int artworkId);
        Task<IEnumerable<ArtworkCommentDTO>> GetArtworkComments(int artworkId);
        Task<IEnumerable<PurchaseDTO>> ListPurchaseArtwork(int UserId);
        Task AddPurchase(PurchaseDTO purchaseDTO);
        Task<IEnumerable<PurchaseDTO>> ListHistoryPurchaseArtwork(int artworkId);
        Task ActiveArtworkStatus(int artworkId);

    }
}
