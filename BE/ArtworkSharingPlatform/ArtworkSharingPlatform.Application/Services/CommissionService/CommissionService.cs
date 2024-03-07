using ArtworkSharingPlatform.Application.Interfaces;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Request;
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
    private readonly ICommissionRequestRepository _commissionRequestRepository;
    private readonly IGenreRepository _genreRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICommissionStatusRepository _commissionStatusRepository;
    private CreateCommissionRequestDTO? _createCommissionRequestDto;
    private CommissionRequest? _commissionRequest;
    private readonly IMapper _mapper;

    public CommissionService(ICommissionRequestRepository repository,
        IMapper mapper,
        IUserRepository userRepository,
        IGenreRepository genreRepository,
        ICommissionStatusRepository commissionStatusRepository
    )
    {
        _commissionRequestRepository = repository;
        _commissionStatusRepository = commissionStatusRepository;
        _mapper = mapper;
        _userRepository = userRepository;
        _genreRepository = genreRepository;
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
        _commissionRequest = _mapper.Map<CommissionRequest>(_createCommissionRequestDto);
        _commissionRequest.RequestDate = DateTime.Now;
        _commissionRequest.IsProgressStatus = 0;
        _commissionRequest.Sender = GetSenderById();
        _commissionRequest.Receiver = GetReceiverById();
        _commissionRequest.Genre = GetGenreById();
        _commissionRequest.CommissionStatus = GetCommissionStatusById();
    }

    private void InsertCommissionEntityToDb()
    {
        _commissionRequestRepository.InsertCommission(_commissionRequest!);
    }

    private User GetSenderById()
    {
        return _userRepository.GetById(_createCommissionRequestDto!.SenderId);
    }

    private Genre GetGenreById()
    {
        return _genreRepository.GetById(_createCommissionRequestDto!.GenreId);
    }

    private User GetReceiverById()
    {
        return _userRepository.GetById(_createCommissionRequestDto!.ReceiverId);
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