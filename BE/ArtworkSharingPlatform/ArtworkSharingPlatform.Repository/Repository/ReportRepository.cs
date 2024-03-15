using ArtworkSharingPlatform.Domain.Entities.Users;
using ArtworkSharingPlatform.Domain.Migrations;
using ArtworkSharingPlatform.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtworkSharingPlatform.Repository.Repository
{
    public class ReportRepository : IReportRepository
    {
        private readonly ArtworkSharingPlatformDbContext _context;
        private readonly IUserRepository _userRepository;

        public ReportRepository(ArtworkSharingPlatformDbContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<Report>> GetAllReports()
        {
            return await _context.Reports.ToListAsync();
        }
        public async Task<Report> GetReportByReportId(int reportId)
        {
                var report = await _context.Reports.Where(x => x.Id == reportId).FirstOrDefaultAsync();
                return report;
        }
        public async Task<IEnumerable<Report>> GetAllUserReport(int userId)
        {
            return await _context.Reports.Where(x => x.ReporterId == userId).ToListAsync();
        }
        public async Task AdminAcceptArtwork (int reportId, bool choice)
        {
            var report = await GetReportByReportId(reportId);
            if ( reportId != null && choice)
            {
                report.Status = "Accepted";
                await _context.SaveChangesAsync();
            } else if (reportId != null)
            {
                report.Status = "Denied";
                await _context.SaveChangesAsync();
            }
        }
    }
}
