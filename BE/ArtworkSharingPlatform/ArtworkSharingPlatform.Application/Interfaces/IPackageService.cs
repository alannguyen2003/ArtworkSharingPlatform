using ArtworkSharingPlatform.DataTransferLayer.Payload.Response;

namespace ArtworkSharingPlatform.Application.Interfaces
{
    public interface IPackageService
    {
        Task<List<PackageInformationDTO>> GetAllPackage();
    }
}