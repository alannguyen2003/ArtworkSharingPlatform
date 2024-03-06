using ArtworkSharingPlatform.DataTransferLayer.Payload.Request;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Response;

namespace ArtworkSharingPlatform.Application.Interfaces;

public interface ICommissionService
{
    CommissionServiceResponse CreateCommission(CreateCommissionRequestDTO createCommissionRequestDto);
}