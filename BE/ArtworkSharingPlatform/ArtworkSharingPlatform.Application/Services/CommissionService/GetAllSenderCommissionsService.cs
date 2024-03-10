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

public class GetAllSenderCommissionsService
{
    private int _senderId;
    private readonly ICommissionRequestRepository _commissionRequestRepository;
    private readonly IUserRepository _userRepository;
    private readonly IGenreRepository _genreRepository;
    private readonly ICommissionStatusRepository _commissionStatusRepository;
    private IEnumerable<CommissionRequest> _commissionRequests;
    private IList<CommissionDTO> _commissionDTOs;
    private readonly IMapper _mapper;

    public GetAllSenderCommissionsService(
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

    public async Task<CommissionServiceResponseDTO> Get(int senderId)
    {
        try
        {
            _senderId = senderId;
            await GetAllCommissionRequestsBySenderId();
            if (!AreCommissionRequestsAvailable()) return NoCommissionsFoundResult();
            await MapCommissionRequestsToCommissionDTOs();
            return GetAllCommissionsSuccessResult();
        }
        catch (Exception e)
        {
            return InternalServerErrorResult(e);
        }
    }


    private Task GetAllCommissionRequestsBySenderId()
    {
        return Task.Run(() => { _commissionRequests = _commissionRequestRepository.GetAllBySenderId(_senderId); });
    }

    private bool AreCommissionRequestsAvailable()
    {
        if (_commissionRequests.Count() == 0) return false;
        return true;
    }

    private async Task MapCommissionRequestsToCommissionDTOs()
    {
        foreach (CommissionRequest commissionRequest in _commissionRequests)
        {
            CommissionDTO commissionDTO = await Task.Run(() => _mapper.Map<CommissionDTO>(commissionRequest));
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

    private CommissionServiceResponseDTO GetAllCommissionsSuccessResult()
    {
        return new CommissionServiceResponseDTO()
        {
            Result = CommissionServiceResult.SUCCESS,
            StatusCode = CommissionServiceStatusCode.SUCCESS,
            Message = $"Successfully retrieved all commissions of Sender with Id = {_senderId} ",
            ReturnData = _commissionDTOs.ToList()
        };
    }

    private CommissionServiceResponseDTO NoCommissionsFoundResult()
    {
        return new CommissionServiceResponseDTO()
        {
            Result = CommissionServiceResult.NO_COMMISSIONS_FOUND,
            StatusCode = CommissionServiceStatusCode.NO_COMMISSIONS_FOUND,
            Message = $"No commissions found for Sender with Id = {_senderId} ",
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