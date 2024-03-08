using ArtworkSharingPlatform.Application.Interfaces;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Request;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Request.CommissionRequest;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Response;
using ArtworkSharingPlatform.Domain.Common.Enum;
using ArtworkSharingPlatform.Domain.Entities.Artworks;
using ArtworkSharingPlatform.Domain.Entities.Commissions;
using ArtworkSharingPlatform.Domain.Entities.Users;
using ArtworkSharingPlatform.Repository.Interfaces;
using AutoMapper;
using Microsoft.Identity.Client;

namespace ArtworkSharingPlatform.Application.Services.CommissionService;

public class CommissionService : ICommissionService
{
    private CreateCommissionService? _createCommissionService;
    private AcceptCommissionService? _acceptCommissionService;
    private RejectCommissionService? _rejectCommissionService;
    private readonly ICommissionRequestRepository _commissionRequestRepository;
    private readonly ICommissionStatusRepository _commissionStatusRepository;
    private CreateCommissionRequestDTO? _createCommissionRequestDto;
    private CommissionRequest? _commissionRequest;
    private readonly IMapper _mapper;

    public CommissionService(
        ICommissionRequestRepository repository,
        IMapper mapper,
        ICommissionStatusRepository commissionStatusRepository)
    {
        _commissionRequestRepository = repository;
        _mapper = mapper;
        _commissionStatusRepository = commissionStatusRepository;
        InitializeChildServices();
    }

    public CommissionServiceResponse CreateCommission(CreateCommissionRequestDTO createCommissionRequestDto)
    {
        return _createCommissionService.Create(createCommissionRequestDto);
    }

    public CommissionServiceResponse AcceptCommission(AcceptCommissionRequestDTO acceptCommissionRequestDto)
    {
        return _acceptCommissionService.Accept(acceptCommissionRequestDto);
    }

    public CommissionServiceResponse RejectCommission(RejectCommissionRequestDTO rejectCommissionRequestDto)
    {
        return _rejectCommissionService.Reject(rejectCommissionRequestDto);
    }

    private void InitializeChildServices()
    {
        _createCommissionService = new CreateCommissionService(
            _commissionRequestRepository,
            _mapper,
            _commissionStatusRepository);
        _acceptCommissionService = new AcceptCommissionService(
            _commissionRequestRepository);
        _rejectCommissionService = new RejectCommissionService(
            _commissionRequestRepository);
    }
}