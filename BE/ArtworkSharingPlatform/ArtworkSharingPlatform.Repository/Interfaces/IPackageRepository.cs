using ArtworkSharingPlatform.Domain.Entities.PackagesInfo;

namespace ArtworkSharingPlatform.Repository.Interfaces
{
    public interface IPackageRepository
    {
        Task<List<PackageInformation>> GetAllPackage();
    }
}