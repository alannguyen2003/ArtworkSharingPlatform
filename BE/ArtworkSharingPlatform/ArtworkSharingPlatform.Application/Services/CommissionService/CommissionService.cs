using ArtworkSharingPlatform.Application.Interfaces;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Request;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Request.Commission;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Response;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Response.Commission;
using ArtworkSharingPlatform.Domain.Entities.Artworks;
using ArtworkSharingPlatform.Domain.Entities.Commissions;
using ArtworkSharingPlatform.Domain.Entities.Users;
using ArtworkSharingPlatform.Repository.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.Identity.Client;

namespace ArtworkSharingPlatform.Application.Services.CommissionService;

public class CommissionService : ICommissionService
{
    private CreateCommissionService? _createCommissionService;
    private AcceptCommissionService? _acceptCommissionService;
    private RejectCommissionService? _rejectCommissionService;
    private GetAllSenderCommissionsService _getAllSenderCommissionsService;
    private GetAllReceiverCommissionsService _getAllReceiverCommissionsService;
    private RequestProgressImageService _requestProgressImageService;
    private RespondProgressImageService _respondProgressImageService;
    private readonly ICommissionRequestRepository _commissionRequestRepository;
    private readonly ICommissionStatusRepository _commissionStatusRepository;
    private readonly IGenreRepository _genreRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly ICommissionImagesRepository _commissionImagesRepository;
    private readonly IMapper _mapper;

    public CommissionService(
        ICommissionRequestRepository repository,
        IMapper mapper,
        ICommissionStatusRepository commissionStatusRepository,
        IUserRepository userRepository,
        IUserRoleRepository userRoleRepository,
        IGenreRepository genreRepository,
        ICommissionImagesRepository commissionImagesRepository)
    {
        _commissionRequestRepository = repository;
        _mapper = mapper;
        _commissionStatusRepository = commissionStatusRepository;
        _userRepository = userRepository;
        _userRoleRepository = userRoleRepository;
        _genreRepository = genreRepository;
        _commissionImagesRepository = commissionImagesRepository;
        InitializeChildServices();
    }

    public CommissionServiceResponseDTO CreateCommission(CreateCommissionRequestDTO createCommissionRequestDto)
    {
        return _createCommissionService.Create(createCommissionRequestDto);
    }

    public CommissionServiceResponseDTO AcceptCommission(AcceptCommissionRequestDTO acceptCommissionRequestDto)
    {
        return _acceptCommissionService.Accept(acceptCommissionRequestDto);
    }

    public CommissionServiceResponseDTO RejectCommission(RejectCommissionRequestDTO rejectCommissionRequestDto)
    {
        return _rejectCommissionService.Reject(rejectCommissionRequestDto);
    }

    public Task<CommissionServiceResponseDTO> GetAllSenderCommissions(int senderId)
    {
        return Task.Run(() => _getAllSenderCommissionsService.Get(senderId));
    }

    public Task<CommissionServiceResponseDTO> GetAllReceiverCommissions(int receiverId)
    {
        return Task.Run(() => _getAllReceiverCommissionsService.Get(receiverId));
    }

    public CommissionServiceResponseDTO RequestProgressImageRequest(RequestProgressImageDTO requestProgressImageDto)
    {
        return _requestProgressImageService.Request(requestProgressImageDto);
    }

    public CommissionServiceResponseDTO RespondProgressImageRequest(RespondProgressImageDTO respondProgressImageDto)
    {
        return _respondProgressImageService.Respond(respondProgressImageDto);
    }

    public async Task<List<CommissionHistoryAdminDTO>> GetAllCommissionAdmin()
    {
        var commission = await _commissionRequestRepository.GetAllCommission();
        var query = commission.AsQueryable();
        return query.ProjectTo<CommissionHistoryAdminDTO>(_mapper.ConfigurationProvider).ToList();
    }

    private void InitializeChildServices()
    {
        _createCommissionService = new CreateCommissionService(
            _commissionRequestRepository,
            _mapper,
            _userRoleRepository,
            _commissionStatusRepository);
        _acceptCommissionService = new AcceptCommissionService(
            _commissionRequestRepository, _userRoleRepository);
        _rejectCommissionService = new RejectCommissionService(
            _commissionRequestRepository,_userRoleRepository);
        _getAllSenderCommissionsService = new GetAllSenderCommissionsService(
            _commissionRequestRepository, _userRepository,
            _genreRepository, _commissionStatusRepository, _mapper
        );
        _getAllReceiverCommissionsService = new GetAllReceiverCommissionsService(
            _commissionRequestRepository, _userRepository,
            _genreRepository, _commissionStatusRepository, _mapper
        );
        _requestProgressImageService = new RequestProgressImageService(
            _commissionRequestRepository
        );
        _respondProgressImageService = new RespondProgressImageService(
            _commissionRequestRepository,
            _commissionImagesRepository
        );
    }
}