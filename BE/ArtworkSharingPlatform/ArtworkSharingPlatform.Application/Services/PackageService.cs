using ArtworkSharingPlatform.Application.Interfaces;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Response;
using ArtworkSharingPlatform.Domain.Entities.PackagesInfo;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtworkSharingPlatform.Repository.Repository.Interfaces;

namespace ArtworkSharingPlatform.Application.Services
{
    public class PackageService : IPackageService
    {
        private readonly IPackageRepository _packageRepository;
        private readonly IMapper _mapper;
        public PackageService(IPackageRepository packageRepository, IMapper mapper)
        {
            _packageRepository = packageRepository;
            _mapper = mapper;
        }

        public async Task<List<PackageInformationDTO>> GetAllPackage()
        {
            var packages = await _packageRepository.GetAllPackage();
            var list = packages.AsQueryable();
            return list.ProjectTo<PackageInformationDTO>(_mapper.ConfigurationProvider).ToList();
        }
        public async Task<PackageInformationDTO> GetPackageById(int id)
        {
            var package = await _packageRepository.GetPackageById(id);
            return _mapper.Map<PackageInformationDTO>(package);
        }
        public async Task UpdatePackage(PackageInformation packageInformation)
        {
            await _packageRepository.UpdatePackage(packageInformation);
        }

        public async Task DeletePackage(int id)
        {
            await _packageRepository.DeletePackage(id);
        }

        public async Task<List<PackageBillingDTO>> GetAllPackageBilling()
        {
            var billing = await _packageRepository.GetAllPackageBilling();
            var list = billing.AsQueryable();
            return list.ProjectTo<PackageBillingDTO>(_mapper.ConfigurationProvider).ToList();
        }

        public async Task<PackageBillingDTO> GetPackageBillingById(int id)
        {
            var billing = await _packageRepository.GetBillingById(id);
            return _mapper.Map<PackageBillingDTO>(billing);
        }

        public async Task<decimal> GetTotalMoneyOfBilling()
        {
            return await _packageRepository.GetTotalPackageBillingAmount();
        }
        public async Task<bool> UserBuyPackage(int userId, int packageId)
        {
            return await _packageRepository.UserBuyPackage(userId, packageId);
        }
    }
}
