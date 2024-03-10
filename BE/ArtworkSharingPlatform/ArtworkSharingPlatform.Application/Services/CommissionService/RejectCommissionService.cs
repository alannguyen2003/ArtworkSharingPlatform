using ArtworkSharingPlatform.DataTransferLayer.Payload.Request.Commission;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Response;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Response.Commission;
using ArtworkSharingPlatform.Domain.Common.Constants;
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
            if (IsNotAcceptedReasonEmpty()) return MissingNotAcceptedReasonResult();
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


    private CommissionServiceResponseDTO RejectCommissionSuccessResult()
    {
        return new CommissionServiceResponseDTO()
        {
            Result = CommissionServiceResult.SUCCESS,
            StatusCode = CommissionServiceStatusCode.SUCCESS,
            Message =
                $"Successfully rejected the commission with Id = {_rejectCommissionRequestDto.CommissionRequestId}" +
                $" and reason = {_rejectCommissionRequestDto.NotAcceptedReason}"
        };
    }

    private CommissionServiceResponseDTO MissingNotAcceptedReasonResult()
    {
        return new CommissionServiceResponseDTO()
        {
            Result = CommissionServiceResult.MISSING_NOT_ACCEPTED_REASON,
            StatusCode = CommissionServiceStatusCode.MISSING_NOT_ACCEPTED_REASON,
            Message = $"The not accepted reason can't be empty. Please try again."
        };
    }

    private CommissionServiceResponseDTO NotAnArtistResult()
    {
        return new CommissionServiceResponseDTO()
        {
            Result = CommissionServiceResult.NOT_AN_ARTIST,
            StatusCode = CommissionServiceStatusCode.NOT_AN_ARTIST,
            Message = $"The user with Id = {_rejectCommissionRequestDto.ReceiverId} " +
                      $"is not a valid Artist. Try selecting another person."
        };
    }

    private CommissionServiceResponseDTO InvalidReceiverResult()
    {
        return new CommissionServiceResponseDTO()
        {
            Result = CommissionServiceResult.INVALID_RECEIVER,
            StatusCode = CommissionServiceStatusCode.INVALID_RECEIVER,
            Message = $"Receiver with Id = {_rejectCommissionRequestDto.ReceiverId} is not allowed to" +
                      $" modify this progress image request."
        };
    }

    private CommissionServiceResponseDTO CommissionRequestNotFoundResult()
    {
        return new CommissionServiceResponseDTO()
        {
            Result = CommissionServiceResult.COMMISSION_REQUEST_NOT_FOUND,
            StatusCode = CommissionServiceStatusCode.COMMISSION_REQUEST_NOT_FOUND,
            Message = $"The commission with Id = {_rejectCommissionRequestDto.CommissionRequestId} is not found!"
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