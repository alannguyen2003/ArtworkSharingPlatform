using ArtworkSharingHost.Extensions;
using ArtworkSharingPlatform.Application.Interfaces;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Request;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Request.Commission;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Response.Commission;
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
    [HttpGet("Sender/GetAll/{senderId}")]
    public async Task<ActionResult> GetAllBySenderId([FromRoute] int senderId)
    {
        
        var result = await _commissionService.GetAllSenderCommissions(senderId);
        var clientResponse = await Task.Run(()=>ReturnStatusCodeToEndpoint(result));
        return clientResponse;
    }
    [HttpGet("Receiver/GetAll/{receiverId}")]
    public async Task<ActionResult> GetAllByReceiverId([FromRoute] int receiverId )
    {
        var result = await _commissionService.GetAllReceiverCommissions(receiverId);
        if (result.Result == CommissionServiceEnum.FAILURE)
        {
            return StatusCode(CommissionServiceStatusCode.INTERNAL_SERVER_ERROR,
                result.Message);
        }
        return StatusCode(CommissionServiceStatusCode.SUCCESS, result);
    }
    [HttpPost("Sender/Request")]
    public ActionResult RequestProgressImage([FromBody] RequestProgressImageDTO requestProgressImageDto)
    {
        requestProgressImageDto.UserId = User.GetUserId();
        var result = _commissionService.RequestProgressImageRequest(requestProgressImageDto);
        var clientResponse = ReturnStatusCodeToEndpoint(result);
        return clientResponse;
    }

    private ObjectResult ReturnStatusCodeToEndpoint(CommissionServiceResponseDTO result)
    {
        if (result.Result == CommissionServiceEnum.FAILURE)
        {
            return StatusCode(CommissionServiceStatusCode.INTERNAL_SERVER_ERROR,
                result.Message);
        }
        return StatusCode(CommissionServiceStatusCode.SUCCESS, result);
    }

}