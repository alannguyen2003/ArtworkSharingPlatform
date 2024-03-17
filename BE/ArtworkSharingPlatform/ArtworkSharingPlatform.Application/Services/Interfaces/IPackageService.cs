using ArtworkSharingPlatform.DataTransferLayer.Payload.Response;
using ArtworkSharingPlatform.Domain.Entities.PackagesInfo;

namespace ArtworkSharingPlatform.Application.Interfaces
{
    public interface IPackageService
    {
        Task<List<PackageInformationDTO>> GetAllPackage();
        Task<PackageInformationDTO> GetPackageById(int id);
        Task UpdatePackage(PackageInformation packageInformation);
        Task DeletePackage(int id);
        Task<List<PackageBillingDTO>> GetAllPackageBilling();
        Task<PackageBillingDTO> GetPackageBillingById(int id);
        Task<decimal> GetTotalMoneyOfBilling();
        Task<bool> UserBuyPackage(int userId, int packageId);
    }
}