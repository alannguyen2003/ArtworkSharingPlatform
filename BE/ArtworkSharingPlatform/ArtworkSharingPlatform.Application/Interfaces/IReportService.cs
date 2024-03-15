using ArtworkSharingPlatform.DataTransferLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtworkSharingPlatform.Application.Interfaces
{
    public interface IReportService
    {
        Task AdminHandleReport(int reportId, bool choice);
        Task<IEnumerable<ReportDTO>> GetAllReport();
        Task<IEnumerable<ReportDTO>> GetAllUserReport(int userId);
        Task<ReportDTO> GetReportById(int reportId);
    }
}
