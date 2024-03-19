using ArtworkSharingPlatform.DataTransferLayer.Payload.Request.ConfigManager;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Response.ConfigManager;

namespace ArtworkSharingPlatform.Repository.Repository;

public class ConfigManagerServiceValidation
{
    private static NewConfigManagerRequest _newConfigManagerRequestDto;
    private static string _validationError;

    public static ConfigManagerServiceResponseValidation IsNewConfigManagerRequestValid(
        NewConfigManagerRequest newConfigManagerRequest)
    {
        _newConfigManagerRequestDto = newConfigManagerRequest;
        if (!IsServicePackageConfigValid() ||
            !AreGeneralConfigsValid() ||
            !ArePagingConfigsValid() ||
            !AreAdvertisementConfigsValid())
            return InvalidNewConfigManagerRequest();
        return ValidNewConfigManagerRequest();
    }

    private static bool IsServicePackageConfigValid()
    {
        if (_newConfigManagerRequestDto.IsServicePackageConfig == null)
        {
            _validationError = "SERVICE_PACKAGE_CONFIG_NOT_SET";
            return false;
        }

        return true;
    }

    private static bool AreGeneralConfigsValid()
    {
        bool generalConfigTrigger = _newConfigManagerRequestDto.IsGeneralConfig;
        if (generalConfigTrigger == null)
        {
            _validationError = "GENERAL_CONFIG_NOT_SET";
            return false;
        }

        if (generalConfigTrigger != null &&
            string.IsNullOrEmpty(_newConfigManagerRequestDto.LogoUrl))
        {
            _validationError = "The LogoUrl must not be null!";
            return false;
        }

        if (generalConfigTrigger != null &&
            string.IsNullOrEmpty(_newConfigManagerRequestDto.MyPhoneNumber))
        {
            _validationError = "My Phone Number must not be null!";
            return false;
        }

        if (generalConfigTrigger != null &&
            string.IsNullOrEmpty(_newConfigManagerRequestDto.Address))
        {
            _validationError = "Address must not be null!";
            return false;
        }

        return true;
    }

    private static bool ArePagingConfigsValid()
    {
        bool pagingConfigTrigger = _newConfigManagerRequestDto.IsPagingConfig;
        if (pagingConfigTrigger == null)
        {
            _validationError = "PAGING_CONFIG_NOT_SET";
            return false;
        }

        if (pagingConfigTrigger != null &&
            _newConfigManagerRequestDto.TotalItemPerPage < 5)
        {
            _validationError = "TotalItemPerPage must not be less than 5!";
            return false;
        }

        if (pagingConfigTrigger != null &&
            _newConfigManagerRequestDto.RowSize < 3)
        {
            _validationError = "RowSize must not be less than 3!";
            return false;
        }

        return true;
    }


    private static bool AreAdvertisementConfigsValid()
    {
        bool advertisementConfigTrigger = _newConfigManagerRequestDto.IsAdvertisementConfig;
        if (advertisementConfigTrigger == null)
        {
            _validationError = "ADVERTISEMENT_CONFIG_NOT_SET";
            return false;
        }

        if (advertisementConfigTrigger != null &&
            string.IsNullOrEmpty(_newConfigManagerRequestDto.CompanyName))
        {
            _validationError = "Company Name must not be null!";
            return false;
        }

        if (advertisementConfigTrigger != null &&
            string.IsNullOrEmpty(_newConfigManagerRequestDto.CompanyEmail))
        {
            _validationError = "Company Email must not be null!";
            return false;
        }

        if (advertisementConfigTrigger != null &&
            string.IsNullOrEmpty(_newConfigManagerRequestDto.CompanyPhoneNumber))
        {
            _validationError = "Company Phone Number must not be null!";
            return false;
        }

        return true;
    }

    private static ConfigManagerServiceResponseValidation InvalidNewConfigManagerRequest()
    {
        return new ConfigManagerServiceResponseValidation()
        {
            Success = false,
            Message = _validationError
        };
    }

    private static ConfigManagerServiceResponseValidation ValidNewConfigManagerRequest()
    {
        return new ConfigManagerServiceResponseValidation()
        {
            Success = true,
            Message = "All fields are validated successfully."
        };
    }
}