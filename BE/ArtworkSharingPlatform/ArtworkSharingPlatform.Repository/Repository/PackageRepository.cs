using ArtworkSharingPlatform.Domain.Entities.PackagesInfo;
using ArtworkSharingPlatform.Domain.Migrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtworkSharingPlatform.Repository.Repository.Interfaces;

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

        public async Task<PackageInformation> GetPackageById(int id)
        {
            PackageInformation package = null;
            try
            {
                package = await _dbContext.PackageInformation.FirstOrDefaultAsync(c => c.Id == id);
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return package;
        }

        public async Task UpdatePackage(PackageInformation packageInformation)
        {
            try
            {
                var package = await GetPackageById(packageInformation.Id);
                if(package != null)
                {
                    package.Name = packageInformation.Name;
                    package.Credit = packageInformation.Credit;
                    package.Price = packageInformation.Price;
                    package.Status = packageInformation.Status;
                    await _dbContext.SaveChangesAsync();
                }
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeletePackage(int id)
        {
            try
            {
                var package = await GetPackageById(id);
                if (package != null)
                {
                    package.Status = 0;
                    await _dbContext.SaveChangesAsync();
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<PackageBilling>> GetAllPackageBilling()
        {
            List<PackageBilling> billing = null;
            try
            {
                billing = await _dbContext.PackageBilling.ToListAsync();
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return billing;
        }

        public async Task<PackageBilling> GetBillingById(int id)
        {
            PackageBilling billing = null;
            try
            {
                billing = await _dbContext.PackageBilling.FirstOrDefaultAsync(b => b.Id == id);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return billing;
        }

        public async Task<decimal> GetTotalPackageBillingAmount()
        {
            decimal totalAmount = 0;
            try
            {
                totalAmount = await _dbContext.PackageBilling
                                    .Select(pb => pb.TotalPrice)
                                    .SumAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return totalAmount;
        }

    }
}
