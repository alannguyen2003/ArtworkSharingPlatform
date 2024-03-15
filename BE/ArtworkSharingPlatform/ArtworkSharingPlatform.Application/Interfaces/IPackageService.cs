using ArtworkSharingPlatform.DataTransferLayer.Payload.Response;
using ArtworkSharingPlatform.Domain.Entities.PackagesInfo;

namespace ArtworkSharingPlatform.Application.Interfaces
{
    public interface IPackageService
    {
        Task<List<PackageInformationDTO>> GetAllPackage();
        Task UpdatePackage(PackageInformation packageInformation);
        Task DeletePackage(int id);
        Task<List<PackageBillingDTO>> GetAllPackageBilling();
        Task<PackageBillingDTO> GetPackageById(int id);
    }
}