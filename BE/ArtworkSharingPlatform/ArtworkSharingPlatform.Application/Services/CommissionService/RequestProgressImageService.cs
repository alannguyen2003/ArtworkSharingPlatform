using ArtworkSharingPlatform.DataTransferLayer.Payload.Request.Commission;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Response.Commission;
using ArtworkSharingPlatform.Domain.Common.Enum;
using ArtworkSharingPlatform.Domain.Entities.Commissions;
using ArtworkSharingPlatform.Repository.Interfaces;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ArtworkSharingPlatform.Application.Services.CommissionService;

public class RequestProgressImageService
{
    private readonly ICommissionRequestRepository _commissionRequestRepository;
    private CommissionRequest _commissionRequest;
    private RequestProgressImageDTO _requestProgressImageDto;
    
    public RequestProgressImageService(ICommissionRequestRepository commissionRequestRepository)
    {
        _commissionRequestRepository = commissionRequestRepository;
    }

    public CommissionServiceResponseDTO Request(RequestProgressImageDTO requestProgressImageDto)
    {
        _requestProgressImageDto = requestProgressImageDto;
        GetCommissionRequestById();
        if (!DoesCommissionRequestExist()) return CommissionRequestNotFoundResult();
        if (!IsSenderValid()) return InvalidSenderResult();
        SetIsProgressStatusTrue();
        return SuccessRequestProgressImageResult();
    }

    private void GetCommissionRequestById()
    {
        _commissionRequest = _commissionRequestRepository.GetById(_requestProgressImageDto.CommissionRequestId);
    }

    private bool IsSenderValid()
    {
        int userId = _requestProgressImageDto.SenderId;
        if (userId == 0 || userId == null ||
            userId != _commissionRequest.SenderId)
            return false;
        return true;
    }

    private bool DoesCommissionRequestExist()
    {
        if (_commissionRequest == null) return false;
        return true;
    }
    private void SetIsProgressStatusTrue()
    {
        _commissionRequest.IsProgressStatus = 1;
        _commissionRequestRepository.Update(_commissionRequest);
    }
    private CommissionServiceResponseDTO SuccessRequestProgressImageResult()
    {
        return new CommissionServiceResponseDTO()
        {
            Result = CommissionServiceEnum.SUCCESS,
            Message = $"Sender with Id = {_commissionRequest.SenderId} " +
                      $"has successfully requested progress image for receiver " +
                      $"with Id = {_commissionRequest.ReceiverId}"
        };
    }
    private CommissionServiceResponseDTO CommissionRequestNotFoundResult()
    {
        return new CommissionServiceResponseDTO()
        {
            Result = CommissionServiceEnum.FAILURE,
            Message = $"The commission with Id = {_requestProgressImageDto.CommissionRequestId} is not found!"
        };
    }
    private CommissionServiceResponseDTO InvalidSenderResult()
    {
        return new CommissionServiceResponseDTO()
        {
            Result = CommissionServiceEnum.FAILURE,
            Message = $"Sender with Id = {_requestProgressImageDto.SenderId} is not allowed to" +
                      $" modify this progress image request."
        };
    }
}