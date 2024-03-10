using ArtworkSharingHost.Extensions;
using ArtworkSharingPlatform.Application.Interfaces;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Request;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Request.Commission;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Response.Commission;
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
        requestDto.SenderId = User.GetUserId();
        var result = _commissionService.CreateCommission(requestDto);
        var clientResponse = ReturnStatusCodeToEndpoint(result);
        return clientResponse;
    }
    [HttpPost("Accept")]
    public ActionResult Accept([FromBody] AcceptCommissionRequestDTO requestDto)
    {
        requestDto.ReceiverId = User.GetUserId();
        var result = _commissionService.AcceptCommission(requestDto);
        var clientResponse = ReturnStatusCodeToEndpoint(result);
        return clientResponse;
    }
    [HttpPost("Reject")]
    public ActionResult Reject([FromBody] RejectCommissionRequestDTO rejectCommissionRequestDto)
    {
        rejectCommissionRequestDto.ReceiverId = User.GetUserId();        
        var result = _commissionService.RejectCommission(rejectCommissionRequestDto);
        var clientResponse = ReturnStatusCodeToEndpoint(result);
        return clientResponse;
    }
    [HttpGet("Sender/GetAll")]
    public async Task<ActionResult> GetAllBySenderId()
    {
        int senderId = User.GetUserId();
        var result = await _commissionService.GetAllSenderCommissions(senderId);
        var clientResponse = await Task.Run(()=>ReturnStatusCodeToEndpoint(result));
        return clientResponse;
    }
    [HttpGet("Receiver/GetAll")]
    public async Task<ActionResult> GetAllByReceiverId()
    {
        int receiverId = User.GetUserId();
        var result = await _commissionService.GetAllReceiverCommissions(receiverId);
        var clientResponse = ReturnStatusCodeToEndpoint(result);
        return clientResponse;
    }
    [HttpPost("Sender/Request")]
    public ActionResult RequestProgressImage([FromBody] RequestProgressImageDTO requestProgressImageDto)
    {
        requestProgressImageDto.SenderId = User.GetUserId();
        var result = _commissionService.RequestProgressImageRequest(requestProgressImageDto);
        var clientResponse = ReturnStatusCodeToEndpoint(result);
        return clientResponse;
    }
    [HttpPost("Receiver/Respond")]
    public ActionResult RespondProgressImage([FromBody] RespondProgressImageDTO respondProgressImageDto)
    {
        respondProgressImageDto.ReceiverId = User.GetUserId();
        var result = _commissionService.RespondProgressImageRequest(respondProgressImageDto);
        var clientResponse = ReturnStatusCodeToEndpoint(result);
        return clientResponse;
    }

    private ObjectResult ReturnStatusCodeToEndpoint(CommissionServiceResponseDTO result)
    {
        return StatusCode(result.StatusCode, result);
    }

}