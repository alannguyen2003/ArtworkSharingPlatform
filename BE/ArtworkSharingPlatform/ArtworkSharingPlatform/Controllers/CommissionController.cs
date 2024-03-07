using ArtworkSharingPlatform.Application.Interfaces;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Request;
using ArtworkSharingPlatform.Domain.Common.Enum;
using Microsoft.AspNetCore.Mvc;

namespace ArtworkSharingHost.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommissionController : ControllerBase
{
    private readonly ICommissionService _commissionService;

    public CommissionController(ICommissionService commissionService)
    {
        _commissionService = commissionService;
    }

    [HttpPost]
    public ActionResult Create([FromBody] CreateCommissionRequestDTO requestDto)
    {
        var result = _commissionService.CreateCommission(requestDto);
        if (result.Result == CommissionServiceEnum.FAILURE)
        {
            return StatusCode(CommissionServiceStatusCode.INTERNAL_SERVER_ERROR,
                result.Message);
        }
        return StatusCode(CommissionServiceStatusCode.SUCCESS, result.Message);
    }
}