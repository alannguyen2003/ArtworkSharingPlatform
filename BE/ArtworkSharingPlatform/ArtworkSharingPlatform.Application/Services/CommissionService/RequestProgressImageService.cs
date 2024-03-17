using ArtworkSharingPlatform.DataTransferLayer.Payload.Request.Commission;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Response.Commission;
using ArtworkSharingPlatform.Domain.Common.Constants;
using ArtworkSharingPlatform.Domain.Entities.Commissions;
using ArtworkSharingPlatform.Repository.Repository.Interfaces;
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
        try
        {
            _requestProgressImageDto = requestProgressImageDto;
            GetCommissionRequestById();
            if (!DoesCommissionRequestExist()) return CommissionRequestNotFoundResult();
            if (!IsSenderValid()) return InvalidSenderResult();
            SetIsProgressStatusTrue();
            return SuccessRequestProgressImageResult();
        }
        catch (Exception e)
        {
            return InternalServerErrorResult(e);
        }
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
            Result = CommissionServiceResult.SUCCESS,
            StatusCode = CommissionServiceStatusCode.SUCCESS,
            Message = $"Sender with Id = {_commissionRequest.SenderId} " +
                      $"has successfully requested progress image for receiver " +
                      $"with Id = {_commissionRequest.ReceiverId}"
        };
    }

    private CommissionServiceResponseDTO CommissionRequestNotFoundResult()
    {
        return new CommissionServiceResponseDTO()
        {
            Result = CommissionServiceResult.COMMISSION_REQUEST_NOT_FOUND,
            StatusCode = CommissionServiceStatusCode.COMMISSION_REQUEST_NOT_FOUND,
            Message = $"The commission with Id = {_requestProgressImageDto.CommissionRequestId} is not found!"
        };
    }

    private CommissionServiceResponseDTO InvalidSenderResult()
    {
        return new CommissionServiceResponseDTO()
        {
            Result = CommissionServiceResult.INVALID_SENDER,
            StatusCode = CommissionServiceStatusCode.INVALID_SENDER,
            Message = $"Sender with Id = {_requestProgressImageDto.SenderId} is not allowed to" +
                      $" modify this progress image request."
        };
    }

    private CommissionServiceResponseDTO InternalServerErrorResult(Exception e)
    {
        return new CommissionServiceResponseDTO()
        {
            Result = CommissionServiceResult.INTERNAL_SERVER_ERROR,
            StatusCode = CommissionServiceStatusCode.INTERNAL_SERVER_ERROR,
            Message = e.Message
        };
    }
}