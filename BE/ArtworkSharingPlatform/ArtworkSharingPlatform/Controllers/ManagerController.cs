using ArtworkSharingPlatform.Application.Interfaces;
using ArtworkSharingPlatform.DataTransferLayer;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Request.Package;
using ArtworkSharingPlatform.Domain.Entities.PackagesInfo;
using ArtworkSharingPlatform.Domain.Entities.Users;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArtworkSharingHost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "RequireManagerRole")]
    public class ManagerController : ControllerBase
    {
        private readonly IPackageService _packageService;
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;
        public ManagerController(IPackageService packageService, IMapper mapper, ITransactionService transactionService)
        {
            _packageService = packageService;
            _mapper = mapper;
            _transactionService = transactionService;
        }

        [HttpGet("packages")]
        public async Task<IActionResult> GetAllPackage()
        {
            return Ok(await _packageService.GetAllPackage());
        }

        [HttpGet("packages/{packageId}")]
        public async Task<IActionResult> GetPackageById(int packageId)
        {
            return Ok(await _packageService.GetPackageById(packageId));
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

        [HttpDelete("packages/{packageId}")]
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

        [HttpGet("packageBilling/{packageBillingId}")]
        public async Task<IActionResult> GetPackageBillingById(int packageBillingId)
        {
            return Ok(await _packageService.GetPackageBillingById(packageBillingId));
        }

        [HttpGet("total")]
        public async Task<IActionResult> GetTotalMoneyOfBilling()
        {
            return Ok(await _packageService.GetTotalMoneyOfBilling());
        }

        [HttpGet("transaction")]
        public async Task<IActionResult> GetAllTransaction()
        {
            return Ok(await _transactionService.GetAllTransaction());
        }

        [HttpGet("transaction/{transactionId}")]
        public async Task<IActionResult> GetTransactionById(int transactionId)
        {
            return Ok(await _transactionService.GetTransactionById(transactionId));
        }
    }
}
