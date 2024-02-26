using ArtworkSharingPlatform.Domain.Entities.Abstract;
using ArtworkSharingPlatform.Domain.Entities.Packages;

namespace ArtworkSharingPlatform.Domain.Entities.Joins;

public class PackageInformationPackageBilling:BaseEntity
{
    private int _packageInformationId;
    private int _packageBillingId;
    public PackageInformation? PackageInformation;
    public PackageBilling? PackageBilling;

    public int PackageInformationId
    {
        get => _packageInformationId;
        set => _packageInformationId = value;
    }

    public int PackageBillingId
    {
        get => _packageBillingId;
        set => _packageBillingId = value;
    }
}