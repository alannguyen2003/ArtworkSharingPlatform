using ArtworkSharingPlatform.DataTransferLayer.Payload.Request.Commission;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Response.Commission;
using ArtworkSharingPlatform.Domain.Common.Constants;
using ArtworkSharingPlatform.Domain.Entities.Commissions;
using ArtworkSharingPlatform.Repository.Interfaces;

namespace ArtworkSharingPlatform.Application.Services.CommissionService;

public class RespondProgressImageService
{
    private readonly ICommissionRequestRepository _commissionRequestRepository;
    private readonly ICommissionImagesRepository _commissionImagesRepository;
    private CommissionRequest _commissionRequest;
    private RespondProgressImageDTO _respondProgressImageDto;


    public RespondProgressImageService(ICommissionRequestRepository commissionRequestRepository,
        ICommissionImagesRepository commissionImagesRepository)
    {
        _commissionRequestRepository = commissionRequestRepository;
        _commissionImagesRepository = commissionImagesRepository;
    }

    public CommissionServiceResponseDTO Respond(RespondProgressImageDTO respondProgressImageDto)
    {
        try
        {
            _respondProgressImageDto = respondProgressImageDto;
            GetCommissionRequestById();
            if (!CommissionRequestExists()) return CommissionRequestNotFoundResult();
            if (!IsReceiverValid()) return InvalidReceiverResult();
            SetIsProgressStatusFalse();
            RemovePreviousProgressImagesFromDb();
            AddProgressImagesToDb();
            return SuccessRespondProgressImageResult();
        }
        catch (Exception e)
        {
            return InternalServerErrorResult(e);
        }
    }

    private void GetCommissionRequestById()
    {
        _commissionRequest = _commissionRequestRepository.GetById(_respondProgressImageDto.CommissionRequestId);
    }

    private bool IsReceiverValid()
    {
        int userId = _respondProgressImageDto.ReceiverId;
        if (userId == 0 || userId == null ||
            userId != _commissionRequest.ReceiverId)
            return false;
        return true;
    }

    private bool CommissionRequestExists()
    {
        if (_commissionRequest == null) return false;
        return true;
    }

    private void SetIsProgressStatusFalse()
    {
        _commissionRequest.IsProgressStatus = 0;
        _commissionRequestRepository.Update(_commissionRequest);
    }

    private void RemovePreviousProgressImagesFromDb()
    {
        _commissionImagesRepository.DeleteAllByCommissionRequestId(_commissionRequest.Id);
    }

    private void AddProgressImagesToDb()
    {
        foreach (string url in _respondProgressImageDto.ImageUrls)
        {
            CommissionImage image = new CommissionImage()
            {
                Url = url,
                IsThumbnail = false,
                PublicId = null,
                CreatedDate = DateTime.Now,
                CommissionRequestId = _commissionRequest.Id
            };
            _commissionImagesRepository.Insert(image);
        }
    }

    private CommissionServiceResponseDTO CommissionRequestNotFoundResult()
    {
        return new CommissionServiceResponseDTO()
        {
            Result = CommissionServiceResult.COMMISSION_REQUEST_NOT_FOUND,
            StatusCode = CommissionServiceStatusCode.COMMISSION_REQUEST_NOT_FOUND,
            Message = $"The commission with Id = {_respondProgressImageDto.CommissionRequestId} is not found!"
        };
    }

    private CommissionServiceResponseDTO SuccessRespondProgressImageResult()
    {
        return new CommissionServiceResponseDTO()
        {
            Result = CommissionServiceResult.SUCCESS,
            StatusCode = CommissionServiceStatusCode.SUCCESS,
            Message = $"Receiver with Id = {_commissionRequest.ReceiverId} " +
                      $"has successfully responded progress image(s) for sender " +
                      $"with Id = {_commissionRequest.SenderId}"
        };
    }
    private CommissionServiceResponseDTO InvalidReceiverResult()
    {
        return new CommissionServiceResponseDTO()
        {
            Result = CommissionServiceResult.INVALID_RECEIVER,
            StatusCode = CommissionServiceStatusCode.INVALID_RECEIVER,
            Message = $"Receiver with Id = {_respondProgressImageDto.ReceiverId} is not allowed to" +
                      $" modify this progress image request."
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