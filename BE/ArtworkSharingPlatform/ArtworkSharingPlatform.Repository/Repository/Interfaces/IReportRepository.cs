using ArtworkSharingPlatform.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtworkSharingPlatform.Repository.Interfaces
{
    public interface IReportRepository
    {
        Task<IEnumerable<Report>> GetAllReports();
        Task<Report> GetReportByReportId(int reportId);
        Task<IEnumerable<Report>> GetAllUserReport(int userId);
        Task AdminAcceptArtwork(int reportId, bool choice);
    }
}
