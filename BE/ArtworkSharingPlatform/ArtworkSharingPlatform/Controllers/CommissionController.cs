using ArtworkSharingPlatform.Application.Interfaces;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Request;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Request.CommissionRequest;
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

    [HttpPost("Create")]
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
    [HttpPost("Accept")]
    public ActionResult Accept([FromBody] AcceptCommissionRequestDTO requestDto)
    {
        var result = _commissionService.AcceptCommission(requestDto);
        if (result.Result == CommissionServiceEnum.FAILURE)
        {
            return StatusCode(CommissionServiceStatusCode.INTERNAL_SERVER_ERROR,
                result.Message);
        }
        return StatusCode(CommissionServiceStatusCode.SUCCESS, result.Message);
    }
    [HttpPost("Reject")]
    public ActionResult Reject([FromBody] RejectCommissionRequestDTO requestDto)
    {
        var result = _commissionService.RejectCommission(requestDto);
        if (result.Result == CommissionServiceEnum.FAILURE)
        {
            return StatusCode(CommissionServiceStatusCode.INTERNAL_SERVER_ERROR,
                result.Message);
        }
        return StatusCode(CommissionServiceStatusCode.SUCCESS, result.Message);
    }
}