using ArtworkSharingPlatform.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArtworkSharingHost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IPackageService _packageService;
        public ManagerController(IPackageService packageService)
        {
            _packageService = packageService;
        }

        [HttpGet("Packages")]
        public async Task<IActionResult> GetAllPackage()
        {
            return Ok(await _packageService.GetAllPackage());
        }
    }
}
