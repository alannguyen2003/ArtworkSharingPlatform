using ArtworkSharingPlatform.Application.Interfaces;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Request;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Response;
using ArtworkSharingPlatform.Domain.Common.Enum;
using ArtworkSharingPlatform.Domain.Entities.Commissions;
using ArtworkSharingPlatform.Repository.Interfaces;
using AutoMapper;

namespace ArtworkSharingPlatform.Application.Services.CommissionService;

public class CommissionService:ICommissionService
{
    private ICommissionRequestRepository _repository;
    private CreateCommissionRequestDTO _createCommissionRequestDto;
    private CommissionRequest? _commissionRequest;
    private IMapper _mapper;
    
    public CommissionService(ICommissionRequestRepository repository,
        IMapper mapper
        )
    {
        _repository = repository;
        _mapper = mapper;
    }
    public CommissionServiceResponse CreateCommission(CreateCommissionRequestDTO createCommissionRequestDto)
    {
        _createCommissionRequestDto = createCommissionRequestDto;
        MapCommissionRequestToCommissionEntity();
        InsertCommissionEntityToDb();
        return CreateCommissionSuccessResult();
    }

    private void MapCommissionRequestToCommissionEntity()
    {
        
    }

    private void InsertCommissionEntityToDb()
    {
        
    }

    private CommissionServiceResponse CreateCommissionSuccessResult()
    {
        return new CommissionServiceResponse()
        {
            Result = CommissionServiceEnum.SUCCESS,
            Message = $"Successfully added the Commission for UserId: {_createCommissionRequestDto.SenderId}"
        };
    }
}