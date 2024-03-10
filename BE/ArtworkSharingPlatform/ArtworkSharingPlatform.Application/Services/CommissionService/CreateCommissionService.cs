using ArtworkSharingPlatform.DataTransferLayer.Payload.Request;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Request.Commission;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Response;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Response.Commission;
using ArtworkSharingPlatform.Domain.Common.Constants;
using ArtworkSharingPlatform.Domain.Entities.Artworks;
using ArtworkSharingPlatform.Domain.Entities.Commissions;
using ArtworkSharingPlatform.Domain.Entities.Users;
using ArtworkSharingPlatform.Repository.Interfaces;
using AutoMapper;
using Microsoft.Identity.Client;

namespace ArtworkSharingPlatform.Application.Services.CommissionService;

public class CreateCommissionService
{
    private readonly ICommissionRequestRepository _commissionRequestRepository;
    private readonly ICommissionStatusRepository _commissionStatusRepository;
    private readonly IUserRoleRepository _userRoleRepository;
    private CreateCommissionRequestDTO? _createCommissionRequestDto;
    private CommissionRequest? _commissionRequest;
    private readonly IMapper _mapper;

    public CreateCommissionService(ICommissionRequestRepository repository,
        IMapper mapper,
        IUserRoleRepository userRoleRepository,
        ICommissionStatusRepository commissionStatusRepository)
    {
        _commissionRequestRepository = repository;
        _commissionStatusRepository = commissionStatusRepository;
        _userRoleRepository = userRoleRepository;
        _mapper = mapper;
    }

    public CommissionServiceResponseDTO Create(CreateCommissionRequestDTO createCommissionRequestDto)
    {
        try
        {
            _createCommissionRequestDto = createCommissionRequestDto;
            if (!IsSenderValid()) return NotAnAudienceResult();
            if (!IsReceiverAnArtist()) return NotAnArtistResult();
            MapCommissionRequestToCommissionEntity();
            InsertCommissionEntityToDb();
            return CreateCommissionSuccessResult();
        }
        catch (Exception e)
        {
            return InternalServerErrorResult(e);
        }
    }

    private bool IsSenderValid()
    {
        List<string> roles = _userRoleRepository.GetRolesByUserId(_createCommissionRequestDto!.SenderId);
        if (roles.Contains("Audience")) return true;
        return false;
    }

    private bool IsReceiverAnArtist()
    {
        List<string> roles = _userRoleRepository.GetRolesByUserId(_createCommissionRequestDto!.ReceiverId);
        if (roles.Contains("Artist")) return true;
        return false;
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

    private CommissionServiceResponseDTO CreateCommissionSuccessResult()
    {
        return new CommissionServiceResponseDTO()
        {
            Result = CommissionServiceResult.SUCCESS,
            StatusCode = CommissionServiceStatusCode.SUCCESS,
            Message = $"Successfully added the Commission " +
                      $"from Sender with Id = {_createCommissionRequestDto.SenderId} " +
                      $"to Receiver with Id = {_createCommissionRequestDto.ReceiverId}"
        };
    }

    private CommissionServiceResponseDTO NotAnAudienceResult()
    {
        return new CommissionServiceResponseDTO()
        {
            Result = CommissionServiceResult.NOT_AN_AUDIENCE,
            StatusCode = CommissionServiceStatusCode.NOT_AN_AUDIENCE,
            Message = $"The user with Id = {_createCommissionRequestDto.SenderId} " +
                      $"is not a valid Audience. Try logging in again as an Audience."
        };
    }

    private CommissionServiceResponseDTO NotAnArtistResult()
    {
        return new CommissionServiceResponseDTO()
        {
            Result = CommissionServiceResult.NOT_AN_ARTIST,
            StatusCode = CommissionServiceStatusCode.NOT_AN_ARTIST,
            Message = $"The user with Id = {_createCommissionRequestDto.ReceiverId} " +
                      $"is not a valid Artist. Try selecting another person."
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