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
    private readonly IUserRoleRepository _userRoleRepository;
    private CommissionRequest? _commissionRequest;

    public RejectCommissionService(ICommissionRequestRepository commissionRequestRepository,
        IUserRoleRepository userRoleRepository)
    {
        _commissionRequestRepository = commissionRequestRepository;
        _userRoleRepository = userRoleRepository;
    }

    public CommissionServiceResponseDTO Reject(RejectCommissionRequestDTO rejectCommissionRequestDto)
    {
        try
        {
            _rejectCommissionRequestDto = rejectCommissionRequestDto;
            GetCommissionRequestById();
            if (!DoesCommissionRequestExist()) return CommissionRequestNotFoundResult();
            if (!IsReceiverValid()) return InvalidReceiverResult();
            if (!IsReceiverAnArtist()) return NotAnArtistResult();
            if (IsNotAcceptedReasonEmpty()) return NotAcceptedReasonEmptyResult();
            ChangeCommissionStatus();
            InsertNotAcceptedReason();
            SaveChanges();
            return RejectCommissionSuccessResult();
        }
        catch (Exception e)
        {
            return InternalServerErrorResult(e);
        }
    }

    private void GetCommissionRequestById()
    {
        _commissionRequest = _commissionRequestRepository.GetById(_rejectCommissionRequestDto!.CommissionRequestId);
    }

    private bool DoesCommissionRequestExist()
    {
        if (_commissionRequest == null) return false;
        return true;
    }

    private bool IsReceiverValid()
    {
        int userId = _rejectCommissionRequestDto.ReceiverId;
        if (userId == 0 || userId == null ||
            userId != _commissionRequest.ReceiverId)
            return false;
        return true;
    }

    private bool IsReceiverAnArtist()
    {
        List<string> roles = _userRoleRepository.GetRolesByUserId(_rejectCommissionRequestDto!.ReceiverId);
        if (roles.Contains("Artist")) return true;
        return false;
    }

    private bool IsNotAcceptedReasonEmpty()
    {
        string notAcceptedReason = _rejectCommissionRequestDto.NotAcceptedReason!;
        if (string.IsNullOrEmpty(notAcceptedReason)) return true;
        return false;
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

    private CommissionServiceResponseDTO NotAnArtistResult()
    {
        return new CommissionServiceResponseDTO()
        {
            Result = CommissionServiceEnum.FAILURE,
            Message = $"The user with Id = {_rejectCommissionRequestDto.ReceiverId} " +
                      $"is not a valid Artist. Try selecting another person."
        };
    }

    private CommissionServiceResponseDTO InvalidReceiverResult()
    {
        return new CommissionServiceResponseDTO()
        {
            Result = CommissionServiceEnum.FAILURE,
            Message = $"Receiver with Id = {_rejectCommissionRequestDto.ReceiverId} is not allowed to" +
                      $" modify this progress image request."
        };
    }

    private CommissionServiceResponseDTO CommissionRequestNotFoundResult()
    {
        return new CommissionServiceResponseDTO()
        {
            Result = CommissionServiceEnum.FAILURE,
            Message = $"The commission with Id = {_rejectCommissionRequestDto.CommissionRequestId} is not found!"
        };
    }

    private CommissionServiceResponseDTO InternalServerErrorResult(Exception e)
    {
        return new CommissionServiceResponseDTO()
        {
            Result = CommissionServiceEnum.FAILURE,
            Message = e.Message
        };
    }
}