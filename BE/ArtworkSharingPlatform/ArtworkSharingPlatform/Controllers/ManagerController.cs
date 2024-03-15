using ArtworkSharingPlatform.Application.Interfaces;
using ArtworkSharingPlatform.DataTransferLayer;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Request.Package;
using ArtworkSharingPlatform.Domain.Entities.PackagesInfo;
using ArtworkSharingPlatform.Domain.Entities.Users;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArtworkSharingHost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IPackageService _packageService;
        private readonly IMapper _mapper;
        public ManagerController(IPackageService packageService, IMapper mapper)
        {
            _packageService = packageService;
            _mapper = mapper;
        }

        [HttpGet("Packages")]
        public async Task<IActionResult> GetAllPackage()
        {
            return Ok(await _packageService.GetAllPackage());
        }
        [HttpPut("updatePackage")]
        public async Task<IActionResult> UpdatePackage([FromBody] PackageUpdate package)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var packageInformation = _mapper.Map<PackageInformation>(package);
                await _packageService.UpdatePackage(packageInformation);
                return Ok("Update successfully");
            }
        }

        [HttpDelete("{packageId}")]
        public async Task<IActionResult> DeletePackage(int packageId)
        {
            await _packageService.DeletePackage(packageId);
            return Ok("Delete successfully");
        }

        [HttpGet("packageBilling")]
        public async Task<IActionResult> GetAllPackageBilling()
        {
            return Ok(await _packageService.GetAllPackageBilling());
        }

        [HttpGet("{packageBillingId}")]
        public async Task<IActionResult> GetPackageBillingById(int packageBillingId)
        {
            return Ok(await _packageService.GetPackageById(packageBillingId));
        }
    }
}
