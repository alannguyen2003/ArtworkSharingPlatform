using ArtworkSharingPlatform.DataTransferLayer.Payload.Request;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Request.Commission;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Response;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Response.Commission;

namespace ArtworkSharingPlatform.Application.Interfaces;

public interface ICommissionService
{
    CommissionServiceResponseDTO CreateCommission(CreateCommissionRequestDTO createCommissionRequestDto);
    CommissionServiceResponseDTO AcceptCommission(AcceptCommissionRequestDTO acceptCommissionRequestDto);
    CommissionServiceResponseDTO RejectCommission(RejectCommissionRequestDTO rejectCommissionRequestDto);
    CommissionServiceResponseDTO CompleteCommission(CompleteCommissionRequestDTO completeCommissionRequestDto);
    Task<CommissionServiceResponseDTO> GetAllSenderCommissions(int senderId);
    Task<CommissionServiceResponseDTO> GetAllReceiverCommissions(int receiverId);
    CommissionServiceResponseDTO RequestProgressImageRequest(RequestProgressImageDTO requestProgressImageDto);
    CommissionServiceResponseDTO RespondProgressImageRequest(RespondProgressImageDTO respondProgressImageDto);
    Task<List<CommissionHistoryAdminDTO>> GetAllCommissionAdmin();
    CommissionHistoryAdminDTO GetSingleCommission(int id);
}