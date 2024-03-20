using ArtworkSharingPlatform.DataTransferLayer.Payload.Request.Commission;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Response.Commission;
using ArtworkSharingPlatform.Domain.Common.Constants;
using ArtworkSharingPlatform.Domain.Entities.Commissions;
using ArtworkSharingPlatform.Domain.Helpers;
using ArtworkSharingPlatform.Repository.Repository.Interfaces;

namespace ArtworkSharingPlatform.Application.Services.CommissionService;

public class CompleteCommissionService
{
    private CompleteCommissionRequestDTO _requestDto;
    private readonly ICommissionRequestRepository _commissionRequestRepository;
    private readonly IUserRoleRepository _userRoleRepository;
    private CommissionRequest? _commissionRequest;

    public CompleteCommissionService(ICommissionRequestRepository repository,
        IUserRoleRepository userRoleRepository)
    {
        _userRoleRepository = userRoleRepository;
        _commissionRequestRepository = repository;
    }

    public CommissionServiceResponseDTO Complete(CompleteCommissionRequestDTO requestDto)
    {
        try
        {
            _requestDto = requestDto;
            GetCommissionRequestById();
            if (!DoesCommissionRequestExist()) return CommissionRequestNotFoundResult();
            if (!IsReceiverValid()) return InvalidReceiverResult();
            if (!IsReceiverAnArtist()) return NotAnArtistResult();
            ChangeCommissionStatus();
            SaveChanges();
            return CompleteCommissionSuccessResult();
        }
        catch (Exception e)
        {
            return InternalServerErrorResult(e);
        }
    }

    private bool DoesCommissionRequestExist()
    {
        if (_commissionRequest == null) return false;
        return true;
    }

    private bool IsReceiverValid()
    {
        int userId = _requestDto.ReceiverId;
        if (userId == 0 || userId == null ||
            userId != _commissionRequest.ReceiverId)
            return false;
        return true;
    }

    private void GetCommissionRequestById()
    {
        _commissionRequest = _commissionRequestRepository.GetById(_requestDto!.CommissionRequestId);
    }

    private bool IsReceiverAnArtist()
    {
        List<string> roles = _userRoleRepository.GetRolesByUserId(_requestDto!.ReceiverId);
        if (roles.Contains("Artist")) return true;
        return false;
    }

    private void ChangeCommissionStatus()
    {
        _commissionRequest!.CommissionStatusId = CommissionStatusConstants.COMPLETED_ID;
    }

    private void SaveChanges()
    {
        _commissionRequestRepository.Update(_commissionRequest);
    }

    private CommissionServiceResponseDTO CompleteCommissionSuccessResult()
    {
        return new CommissionServiceResponseDTO()
        {
            Result = CommissionServiceResult.SUCCESS,
            StatusCode = CommissionServiceStatusCode.SUCCESS,
            Message = $"Successfully completed the Commission " +
                      $"between Sender with Id = {_commissionRequest!.SenderId} " +
                      $"and Receiver with Id = {_commissionRequest!.ReceiverId}"
        };
    }

    private CommissionServiceResponseDTO NotAnArtistResult()
    {
        return new CommissionServiceResponseDTO()
        {
            Result = CommissionServiceResult.NOT_AN_ARTIST,
            StatusCode = CommissionServiceStatusCode.NOT_AN_ARTIST,
            Message = $"The user with Id = {_requestDto.ReceiverId} " +
                      $"is not a valid Artist. Try selecting another person."
        };
    }


    private CommissionServiceResponseDTO InvalidReceiverResult()
    {
        return new CommissionServiceResponseDTO()
        {
            Result = CommissionServiceResult.INVALID_RECEIVER,
            StatusCode = CommissionServiceStatusCode.INVALID_RECEIVER,
            Message = $"Receiver with Id = {_requestDto.ReceiverId} is not allowed to" +
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

    private CommissionServiceResponseDTO CommissionRequestNotFoundResult()
    {
        return new CommissionServiceResponseDTO()
        {
            Result = CommissionServiceResult.COMMISSION_REQUEST_NOT_FOUND,
            StatusCode = CommissionServiceStatusCode.COMMISSION_REQUEST_NOT_FOUND,
            Message = $"The commission with Id = {_requestDto.CommissionRequestId} is not found!"
        };
    }
}