using ArtworkSharingPlatform.DataTransferLayer;
using ArtworkSharingPlatform.Domain.Helpers;

namespace ArtworkSharingPlatform.Application.Interfaces
{
	public interface IArtworkService
	{
		Task<PagedList<ArtworkDTO>> GetArtworksAsync(UserParams userParams);
	}
}
