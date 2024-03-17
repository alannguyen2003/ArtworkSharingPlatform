using ArtworkSharingPlatform.Domain.Entities.PackagesInfo;
using ArtworkSharingPlatform.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtworkSharingPlatform.DataTransferLayer.Payload.Response
{
    public class ConfigManagerAdminDTO
    {
        public int Id { get; set; }
        public DateTime ConfigDate { get; set; }
        public Boolean IsServicePackageConfig { get; set; }
        public Boolean IsPhysicalImageConfig { get; set; }
        public int MaxReleaseCount { get; set; }
        public Boolean IsGeneralConfig { get; set; }
        public string? LogoUrl { get; set; }
        public string? MyPhoneNumber { get; set; }
        public string? Address { get; set; }
        public Boolean IsPagingConfig { get; set; }
        public int TotalItemPerPage { get; set; }
        public int RowSize { get; set; }
        public Boolean IsAdvertisementConfig { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyPhoneNumber { get; set; }
        public string? CompanyEmail { get; set; }
        public string? Administrator { get; set; }
        public ICollection<PackageInformation>? PackageConfigs { get; set; }

    }
}
