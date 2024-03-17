using ArtworkSharingPlatform.DataTransferLayer.Payload.Request;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Request.Commission;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Response;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Response.Commission;
using ArtworkSharingPlatform.Domain.Common.Constants;
using ArtworkSharingPlatform.Domain.Entities.Commissions;
using ArtworkSharingPlatform.Domain.Entities.Users;
using ArtworkSharingPlatform.Domain.Helpers;
using ArtworkSharingPlatform.Repository.Repository.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Identity.Client;

namespace ArtworkSharingPlatform.Application.Services.CommissionService;

public class AcceptCommissionService
{
    private AcceptCommissionRequestDTO? _acceptCommissionRequestDto;
    private readonly ICommissionRequestRepository _commissionRequestRepository;
    private readonly IUserRoleRepository _userRoleRepository;
    private CommissionRequest? _commissionRequest;
    private decimal _minPrice;
    private decimal _maxPrice;
    private decimal _actualPrice;

    public AcceptCommissionService(ICommissionRequestRepository repository,
        IUserRoleRepository userRoleRepository
    )
    {
        _userRoleRepository = userRoleRepository;
        _commissionRequestRepository = repository;
    }

    public CommissionServiceResponseDTO Accept(AcceptCommissionRequestDTO acceptCommissionRequestDto)
    {
        try
        {
            _acceptCommissionRequestDto = acceptCommissionRequestDto;
            GetCommissionRequestById();
            if (!DoesCommissionRequestExist()) return CommissionRequestNotFoundResult();
            if (!IsReceiverValid()) return InvalidReceiverResult();
            if (!IsReceiverAnArtist()) return NotAnArtistResult();
            if (!IsActualPriceBetweenMinAndMaxPrice())
                return ActualPriceNotWithinMinAndMaxPriceResult();
            ChangeCommissionStatus();
            InsertActualPrice();
            SaveChanges();
            return AcceptCommissionSuccessResult();
        }
        catch (Exception e)
        {
            return InternalServerErrorResult(e);
        }
    }

    private void GetCommissionRequestById()
    {
        _commissionRequest = _commissionRequestRepository.GetById(_acceptCommissionRequestDto!.CommissionRequestId);
    }

    private bool DoesCommissionRequestExist()
    {
        if (_commissionRequest == null) return false;
        return true;
    }

    private bool IsReceiverValid()
    {
        int userId = _acceptCommissionRequestDto.ReceiverId;
        if (userId == 0 || userId == null ||
            userId != _commissionRequest.ReceiverId)
            return false;
        return true;
    }

    private bool IsReceiverAnArtist()
    {
        List<string> roles = _userRoleRepository.GetRolesByUserId(_acceptCommissionRequestDto!.ReceiverId);
        if (roles.Contains("Artist")) return true;
        return false;
    }

    private bool IsActualPriceBetweenMinAndMaxPrice()
    {
        _minPrice = _commissionRequest!.MinPrice;
        _maxPrice = _commissionRequest!.MaxPrice;
        _actualPrice = _acceptCommissionRequestDto!.ActualPrice;
        if (_minPrice <= _actualPrice && _actualPrice <= _maxPrice) return true;
        return false;
    }


    private void ChangeCommissionStatus()
    {
        _commissionRequest!.CommissionStatusId = CommissionStatusConstants.ACCEPTED_ID;
    }

    private void InsertActualPrice()
    {
        _commissionRequest.ActualPrice = _acceptCommissionRequestDto.ActualPrice;
    }

    private void SaveChanges()
    {
        _commissionRequestRepository.Update(_commissionRequest);
    }

    private CommissionServiceResponseDTO ActualPriceNotWithinMinAndMaxPriceResult()
    {
        return new CommissionServiceResponseDTO()
        {
            Result = CommissionServiceResult.INVALID_ACTUAL_PRICE,
            StatusCode = CommissionServiceStatusCode.INVALID_ACTUAL_PRICE,
            Message =
                $"The Actual Price ({_actualPrice}) is not within the Min Price " +
                $"({_minPrice}) and Max Price ({_maxPrice}) range. Please try again."
        };
    }

    private CommissionServiceResponseDTO AcceptCommissionSuccessResult()
    {
        return new CommissionServiceResponseDTO()
        {
            Result = CommissionServiceResult.SUCCESS,
            StatusCode = CommissionServiceStatusCode.SUCCESS,
            Message = $"Successfully accepted the Commission " +
                      $"from Sender with Id = {_commissionRequest!.SenderId} " +
                      $"to Receiver with Id = {_commissionRequest!.ReceiverId}" +
                      $"with the final price = {_acceptCommissionRequestDto!.ActualPrice}"
        };
    }

    private CommissionServiceResponseDTO NotAnArtistResult()
    {
        return new CommissionServiceResponseDTO()
        {
            Result = CommissionServiceResult.NOT_AN_ARTIST,
            StatusCode = CommissionServiceStatusCode.NOT_AN_ARTIST,
            Message = $"The user with Id = {_acceptCommissionRequestDto.ReceiverId} " +
                      $"is not a valid Artist. Try selecting another person."
        };
    }

    private CommissionServiceResponseDTO InvalidReceiverResult()
    {
        return new CommissionServiceResponseDTO()
        {
            Result = CommissionServiceResult.INVALID_RECEIVER,
            StatusCode = CommissionServiceStatusCode.INVALID_RECEIVER,
            Message = $"Receiver with Id = {_acceptCommissionRequestDto.ReceiverId} is not allowed to" +
                      $" modify this progress image request."
        };
    }

    private CommissionServiceResponseDTO CommissionRequestNotFoundResult()
    {
        return new CommissionServiceResponseDTO()
        {
            Result = CommissionServiceResult.COMMISSION_REQUEST_NOT_FOUND,
            StatusCode = CommissionServiceStatusCode.COMMISSION_REQUEST_NOT_FOUND,
            Message = $"The commission with Id = {_acceptCommissionRequestDto.CommissionRequestId} is not found!"
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