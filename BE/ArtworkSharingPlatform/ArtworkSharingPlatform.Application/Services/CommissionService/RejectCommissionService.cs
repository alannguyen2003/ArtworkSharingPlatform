using ArtworkSharingPlatform.DataTransferLayer.Payload.Request.Commission;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Response;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Response.Commission;
using ArtworkSharingPlatform.Domain.Common.Enum;
using ArtworkSharingPlatform.Domain.Entities.Commissions;
using ArtworkSharingPlatform.Domain.Helpers;
using ArtworkSharingPlatform.Repository.Interfaces;

namespace ArtworkSharingPlatform.Application.Services.CommissionService;

public class RejectCommissionService
{
    private RejectCommissionRequestDTO _rejectCommissionRequestDto;
    private readonly ICommissionRequestRepository _commissionRequestRepository;
    private CommissionRequest? _commissionRequest;

    public RejectCommissionService(ICommissionRequestRepository repository)
    {
        _commissionRequestRepository = repository;
    }

    public CommissionServiceResponseDTO Reject(RejectCommissionRequestDTO rejectCommissionRequestDto)
    {
        _rejectCommissionRequestDto = rejectCommissionRequestDto;
        if (IsNotAcceptedReasonEmpty()) return NotAcceptedReasonEmptyResult();
        GetCommissionRequestById();
        ChangeCommissionStatus();
        InsertNotAcceptedReason();
        SaveChanges();
        return RejectCommissionSuccessResult();
    }

    private bool IsNotAcceptedReasonEmpty()
    {
        string notAcceptedReason = _rejectCommissionRequestDto.NotAcceptedReason!;
        if (string.IsNullOrEmpty(notAcceptedReason)) return true;
        return false;
    }

    private void GetCommissionRequestById()
    {
        _commissionRequest = _commissionRequestRepository.GetById(_rejectCommissionRequestDto!.CommissionRequestId);
    }

    private void ChangeCommissionStatus()
    {
        _commissionRequest!.CommissionStatusId = CommissionStatusConstants.REJECTED_ID;
    }

    private void InsertNotAcceptedReason()
    {
        _commissionRequest.NotAcceptedReason = _rejectCommissionRequestDto.NotAcceptedReason;
    }

    private void SaveChanges()
    {
        _commissionRequestRepository.Update(_commissionRequest);
    }

    private CommissionServiceResponseDTO NotAcceptedReasonEmptyResult()
    {
        return new CommissionServiceResponseDTO()
        {
            Result = CommissionServiceEnum.FAILURE,
            Message = $"The not accepted reason can't be empty. Please try again."
        };
    }

    private CommissionServiceResponseDTO RejectCommissionSuccessResult()
    {
        return new CommissionServiceResponseDTO()
        {
            Result = CommissionServiceEnum.SUCCESS,
            Message =
                $"Successfully rejected the commission with Id = {_rejectCommissionRequestDto.CommissionRequestId}" +
                $" and reason = {_rejectCommissionRequestDto.NotAcceptedReason}"
        };
    }
}