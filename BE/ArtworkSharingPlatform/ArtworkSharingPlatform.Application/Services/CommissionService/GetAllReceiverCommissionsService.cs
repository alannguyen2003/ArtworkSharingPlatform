using ArtworkSharingPlatform.DataTransferLayer.Payload.Response;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Response.Commission;
using ArtworkSharingPlatform.Domain.Common.Enum;
using ArtworkSharingPlatform.Domain.Entities.Artworks;
using ArtworkSharingPlatform.Domain.Entities.Commissions;
using ArtworkSharingPlatform.Domain.Entities.Users;
using ArtworkSharingPlatform.Repository.Interfaces;
using AutoMapper;

namespace ArtworkSharingPlatform.Application.Services.CommissionService;

public class GetAllReceiverCommissionsService
{
    private int _receiverId;
    private readonly ICommissionRequestRepository _commissionRequestRepository;
    private readonly IUserRepository _userRepository;
    private readonly IGenreRepository _genreRepository;
    private readonly ICommissionStatusRepository _commissionStatusRepository;
    private IEnumerable<CommissionRequest> _commissionRequests;
    private IList<CommissionDTO> _commissionDTOs;
    private readonly IMapper _mapper;

    public GetAllReceiverCommissionsService(
        ICommissionRequestRepository commissionRequestRepository,
        IUserRepository userRepository,
        IGenreRepository genreRepository,
        ICommissionStatusRepository commissionStatusRepository,
        IMapper mapper)
    {
        _commissionRequestRepository = commissionRequestRepository;
        _mapper = mapper;
        _userRepository = userRepository;
        _genreRepository = genreRepository;
        _commissionStatusRepository = commissionStatusRepository;
        InitializeObjects();
    }

    public async Task<CommissionServiceResponseDTO> Get(int receiverId)
    {
        _receiverId = receiverId;
        await GetAllCommissionRequestsByReceiverId();
        await MapCommissionRequestsToCommissionDTOs();
        return GetAllCommissionsSuccessResult();
    }

    private Task GetAllCommissionRequestsByReceiverId()
    {
        return Task.Run(() => _commissionRequests = _commissionRequestRepository.GetAllByReceiverId(_receiverId));
    }

    private CommissionServiceResponseDTO GetAllCommissionsSuccessResult()
    {
        return new CommissionServiceResponseDTO()
        {
            Result = CommissionServiceEnum.SUCCESS,
            Message = $"Successfully retrieved all commissions of Receiver with Id = {_receiverId} ",
            ReturnData = _commissionDTOs.ToList()
        };
    }

    private async Task MapCommissionRequestsToCommissionDTOs()
    {
        foreach (CommissionRequest commissionRequest in _commissionRequests)
        {
            CommissionDTO commissionDTO = await Task.Run(()=>_mapper.Map<CommissionDTO>(commissionRequest));
            commissionDTO.SenderName = await GetSenderNameById(commissionRequest.SenderId);
            commissionDTO.ReceiverName = await GetReceiverNameById(commissionRequest.ReceiverId);
            await Task.Run(() =>
            {
                commissionDTO.GenreName = GetGenreNameById(commissionRequest.GenreId);
                commissionDTO.CommissionStatus = GetCommissionStatusById(commissionRequest.CommissionStatusId);
                _commissionDTOs.Add(commissionDTO);
            });
        }
    }

    private async Task<string> GetSenderNameById(int senderId)
    {
        User sender = await _userRepository.GetUserById(senderId);
        return sender.Name;
    }

    private async Task<string> GetReceiverNameById(int receiverId)
    {
        User receiver = await _userRepository.GetUserById(receiverId);
        return receiver.Name;
    }

    private string GetGenreNameById(int genreId)
    {
        Genre genre = _genreRepository.GetById(genreId);
        return genre.Name;
    }

    private string GetCommissionStatusById(int commissionStatusId)
    {
        CommissionStatus commissionStatus = _commissionStatusRepository.GetById(commissionStatusId);
        return commissionStatus.Description;
    }

    private void InitializeObjects()
    {
        _commissionDTOs = new List<CommissionDTO>();
    }
}