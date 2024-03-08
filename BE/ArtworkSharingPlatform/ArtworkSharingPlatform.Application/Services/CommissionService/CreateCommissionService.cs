using ArtworkSharingPlatform.DataTransferLayer.Payload.Request;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Request.CommissionRequest;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Response;
using ArtworkSharingPlatform.Domain.Common.Enum;
using ArtworkSharingPlatform.Domain.Entities.Artworks;
using ArtworkSharingPlatform.Domain.Entities.Commissions;
using ArtworkSharingPlatform.Domain.Entities.Users;
using ArtworkSharingPlatform.Repository.Interfaces;
using AutoMapper;

namespace ArtworkSharingPlatform.Application.Services.CommissionService;

public class CreateCommissionService
{
    private readonly ICommissionRequestRepository _commissionRequestRepository;
    private readonly ICommissionStatusRepository _commissionStatusRepository;
    private CreateCommissionRequestDTO? _createCommissionRequestDto;
    private CommissionRequest? _commissionRequest;
    private readonly IMapper _mapper;

    public CreateCommissionService(ICommissionRequestRepository repository,
        IMapper mapper,
        ICommissionStatusRepository commissionStatusRepository)
    {
        _commissionRequestRepository = repository;
        _commissionStatusRepository = commissionStatusRepository;
        _mapper = mapper;
    }
    public CommissionServiceResponse Create(CreateCommissionRequestDTO createCommissionRequestDto)
    {
        _createCommissionRequestDto = createCommissionRequestDto;
        MapCommissionRequestToCommissionEntity();
        InsertCommissionEntityToDb();
        return CreateCommissionSuccessResult();
    }

    private void MapCommissionRequestToCommissionEntity()
    {
        _commissionRequest = _mapper.Map<CommissionRequest>(_createCommissionRequestDto);
        _commissionRequest.RequestDate = DateTime.Now;
        _commissionRequest.IsProgressStatus = 0;
        _commissionRequest.SenderId = _createCommissionRequestDto.SenderId;
        _commissionRequest.ReceiverId = _createCommissionRequestDto.ReceiverId;
        _commissionRequest.GenreId = _createCommissionRequestDto.GenreId;
        _commissionRequest.CommissionStatus = GetCommissionStatusById();
    }

    private void InsertCommissionEntityToDb()
    {
        _commissionRequestRepository.Insert(_commissionRequest!);
    }
    
    private CommissionStatus GetCommissionStatusById()
    {
        return _commissionStatusRepository.GetById(1);
    }

    private CommissionServiceResponse CreateCommissionSuccessResult()
    {
        return new CommissionServiceResponse()
        {
            Result = CommissionServiceEnum.SUCCESS,
            Message = $"Successfully added the Commission " +
                      $"from Sender with Id = {_createCommissionRequestDto.SenderId} " +
                      $"to Receiver with Id = {_createCommissionRequestDto.ReceiverId}"
        };
    }
}