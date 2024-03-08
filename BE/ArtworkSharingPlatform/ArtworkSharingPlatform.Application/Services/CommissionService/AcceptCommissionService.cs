using ArtworkSharingPlatform.DataTransferLayer.Payload.Request;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Request.CommissionRequest;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Response;
using ArtworkSharingPlatform.Domain.Common.Enum;
using ArtworkSharingPlatform.Domain.Entities.Commissions;
using ArtworkSharingPlatform.Domain.Entities.Users;
using ArtworkSharingPlatform.Domain.Helpers;
using ArtworkSharingPlatform.Repository.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Identity.Client;

namespace ArtworkSharingPlatform.Application.Services.CommissionService;

public class AcceptCommissionService
{
    private AcceptCommissionRequestDTO? _acceptCommissionRequestDto;
    private readonly ICommissionRequestRepository _commissionRequestRepository;
    private CommissionRequest? _commissionRequest;
    private decimal _minPrice;
    private decimal _maxPrice;
    private decimal _actualPrice;

    public AcceptCommissionService(ICommissionRequestRepository repository
    )
    {
        _commissionRequestRepository = repository;
    }

    public CommissionServiceResponse Accept(AcceptCommissionRequestDTO acceptCommissionRequestDto)
    {
        _acceptCommissionRequestDto = acceptCommissionRequestDto;
        GetCommissionRequestById();
        if (!IsActualPriceBetweenMinAndMaxPrice())
            return ActualPriceNotWithinMinAndMaxPriceResult();
        ChangeCommissionStatus();
        InsertActualPrice();
        SaveChanges();
        return AcceptCommissionSuccessResult();
    }

    private bool IsActualPriceBetweenMinAndMaxPrice()
    {
        _minPrice = _commissionRequest!.MinPrice;
        _maxPrice = _commissionRequest!.MaxPrice;
        _actualPrice = _acceptCommissionRequestDto!.ActualPrice;
        if (_minPrice <= _actualPrice && _actualPrice <= _maxPrice) return true;
        return false;
    }

    private void GetCommissionRequestById()
    {
        _commissionRequest = _commissionRequestRepository.GetById(_acceptCommissionRequestDto!.CommissionRequestId);
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

    private CommissionServiceResponse ActualPriceNotWithinMinAndMaxPriceResult()
    {
        return new CommissionServiceResponse()
        {
            Result = CommissionServiceEnum.FAILURE,
            Message =
                $"The Actual Price ({_actualPrice}) is not within the Min Price " +
                $"({_minPrice}) and Max Price ({_maxPrice}) range. Please try again."
        };
    }

    private CommissionServiceResponse AcceptCommissionSuccessResult()
    {
        return new CommissionServiceResponse()
        {
            Result = CommissionServiceEnum.SUCCESS,
            Message = $"Successfully accepted the Commission " +
                      $"from Sender with Id = {_commissionRequest!.SenderId} " +
                      $"to Receiver with Id = {_commissionRequest!.ReceiverId}" +
                      $"with the final price = {_acceptCommissionRequestDto!.ActualPrice}"
        };
    }
}