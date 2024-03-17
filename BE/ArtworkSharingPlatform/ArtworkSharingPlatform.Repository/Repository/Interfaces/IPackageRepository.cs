using ArtworkSharingPlatform.Domain.Entities.PackagesInfo;

namespace ArtworkSharingPlatform.Repository.Repository.Interfaces
{
    public interface IPackageRepository
    {
        Task<List<PackageInformation>> GetAllPackage();
        Task<PackageInformation> GetPackageById(int id);
        Task UpdatePackage(PackageInformation packageInformation);
        Task DeletePackage(int id);
    }
}