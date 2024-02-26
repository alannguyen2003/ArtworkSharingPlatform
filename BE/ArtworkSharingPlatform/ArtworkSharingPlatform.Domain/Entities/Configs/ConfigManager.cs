using ArtworkSharingPlatform.Domain.Entities.Abstract;
using ArtworkSharingPlatform.Domain.Entities.Packages;
using ArtworkSharingPlatform.Domain.Entities.Users;

namespace ArtworkSharingPlatform.Domain.Entities.Configs;

public class ConfigManager : BaseEntity
{
    private int _administratorId;
    private DateTime _configDate;
    private Boolean _isServicePackageConfig;
    private Boolean _isPhysicalImageConfig;
    private int _maxReleaseCount;
    private Boolean _isGeneralConfig;
    private string? _logoUrl;
    private string? _myPhoneNumber;
    private string? _address;
    private Boolean _isPagingConfig;
    private int _totalItemPerPage;
    private int _rowSize;
    private Boolean _isAdvertisementConfig;
    private string? _companyName;
    private string? _companyPhoneNumber;
    private string? _companyEmail;
    public Administrator? Administrator;
    public ICollection<PackageInformation>? PackageConfigs;

    public int AdministratorId
    {
        get => _administratorId;
        set => _administratorId = value;
    }

    public DateTime ConfigDate
    {
        get => _configDate;
        set => _configDate = value;
    }

    public bool IsServicePackageConfig
    {
        get => _isServicePackageConfig;
        set => _isServicePackageConfig = value;
    }

    public bool IsPhysicalImageConfig
    {
        get => _isPhysicalImageConfig;
        set => _isPhysicalImageConfig = value;
    }

    public int MaxReleaseCount
    {
        get => _maxReleaseCount;
        set => _maxReleaseCount = value;
    }

    public bool IsGeneralConfig
    {
        get => _isGeneralConfig;
        set => _isGeneralConfig = value;
    }

    public string LogoUrl
    {
        get => _logoUrl;
        set => _logoUrl = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string MyPhoneNumber
    {
        get => _myPhoneNumber;
        set => _myPhoneNumber = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Address
    {
        get => _address;
        set => _address = value ?? throw new ArgumentNullException(nameof(value));
    }

    public bool IsPagingConfig
    {
        get => _isPagingConfig;
        set => _isPagingConfig = value;
    }

    public int TotalItemPerPage
    {
        get => _totalItemPerPage;
        set => _totalItemPerPage = value;
    }

    public int RowSize
    {
        get => _rowSize;
        set => _rowSize = value;
    }

    public bool IsAdvertisementConfig
    {
        get => _isAdvertisementConfig;
        set => _isAdvertisementConfig = value;
    }

    public string CompanyName
    {
        get => _companyName;
        set => _companyName = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string CompanyPhoneNumber
    {
        get => _companyPhoneNumber;
        set => _companyPhoneNumber = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string CompanyEmail
    {
        get => _companyEmail;
        set => _companyEmail = value ?? throw new ArgumentNullException(nameof(value));
    }
}