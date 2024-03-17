using ArtworkSharingHost.Extensions;
using ArtworkSharingPlatform.Application.Interfaces;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Request.ConfigManager;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Response.ConfigManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArtworkSharingHost.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ConfigManagerController : ControllerBase
{
    private readonly IConfigService _configManagerService;

    public ConfigManagerController(IConfigService configManagerService)
    {
        _configManagerService = configManagerService;
    }
    
    [HttpPost("Create")]
    public ActionResult Create([FromBody] NewConfigManagerRequest newConfigManagerRequest)
    {
        newConfigManagerRequest.AdministratorId = User.GetUserId();
        var result = _configManagerService.Create(newConfigManagerRequest);
        var clientResponse = ReturnStatusCodeToEndpoint(result);
        return clientResponse;
    }
    private ObjectResult ReturnStatusCodeToEndpoint(ConfigManagerServiceResponse result)
    {
        return StatusCode(result.StatusCode, result);
    }
}