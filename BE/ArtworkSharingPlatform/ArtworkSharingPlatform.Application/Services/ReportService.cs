using ArtworkSharingPlatform.Application.Interfaces;
using ArtworkSharingPlatform.DataTransferLayer;
using ArtworkSharingPlatform.Domain.Entities.Artworks;
using ArtworkSharingPlatform.Repository.Interfaces;
using AutoMapper;
using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtworkSharingPlatform.Application.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;

        public ReportService(IReportRepository reportRepository, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ReportDTO>> GetAllReport()
        {

            var report = await _reportRepository.GetAllReports();
            var reportDTOs = _mapper.Map<IList<ReportDTO>>(report);
            return reportDTOs;
        }
        public async Task<IEnumerable<ReportDTO>> GetAllUserReport(int userId)
        {

            var report = await _reportRepository.GetAllUserReport(userId);
            var reportDTOs = _mapper.Map<IList<ReportDTO>>(report);
            return reportDTOs;
        }
        public async Task<ReportDTO> GetReportById(int reportId)
        {

            var report = await _reportRepository.GetReportByReportId(reportId);
            var reportDTOs = _mapper.Map<ReportDTO>(report);
            return reportDTOs;
        }
        public async Task AdminHandleReport(int reportId, bool choice)
        {
            var report = await _reportRepository.GetReportByReportId(reportId);
            await _reportRepository.AdminAcceptArtwork(reportId, choice);
        }
    }
}
