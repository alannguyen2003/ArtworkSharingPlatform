using ArtworkSharingPlatform.Domain.Entities.PackagesInfo;
using ArtworkSharingPlatform.Domain.Migrations;
using ArtworkSharingPlatform.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtworkSharingPlatform.Repository.Repository
{
    public class PackageRepository : IPackageRepository
    {
        private readonly ArtworkSharingPlatformDbContext _dbContext;
        public PackageRepository(ArtworkSharingPlatformDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<PackageInformation>> GetAllPackage()
        {
            List<PackageInformation> packages = null;
            try
            {
                packages = await _dbContext.PackageInformation.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return packages;
        }
    }
}
