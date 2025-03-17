using CapaciConnectBackend.Context;
using CapaciConnectBackend.DTOS;
using CapaciConnectBackend.DTOS.Reports;
using CapaciConnectBackend.Models.Domain;
using CapaciConnectBackend.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace CapaciConnectBackend.Services.Services
{
    public class ReportService : IReports
    {
        private readonly AplicationDBContext _context;
        private readonly IConfiguration _configuration;
        public ReportService(AplicationDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<Reports>> GetAllReportsAsync()
        {
            var reports = await _context.Reports.AsNoTracking().ToListAsync();

            return reports;
        }

        public async Task<List<Reports>> GetReportsByWorkshopIdAsync(int workshopId)
        {
            var report = await _context.Reports.Where(r => r.Id_workshop_id == workshopId).AsNoTracking().ToListAsync();

            return report;
        }

        public async Task<Reports?> CreateReportAsync(ReportDTO reportDTO, int userId)
        {
            var exists = await _context.Reports.AnyAsync(r => r.Tittle == reportDTO.Tittle);

            if (exists) return null;

            var newReport = new Reports
            {
                Tittle = reportDTO.Tittle,
                Content = reportDTO.Content,
                Id_workshop_id = reportDTO.Id_workshop_id,
                Id_user_id = userId,
            };

            _context.Reports.Add(newReport);
            await _context.SaveChangesAsync();

            return newReport;
        }

        public async Task<Reports?> UpdateReportAsync(int reportId, UpdateReportDTO reportDTO)
        {
            var report = await _context.Reports.FindAsync(reportId);

            if (report == null) return null;

            report.Tittle = reportDTO.Tittle;
            report.Content = reportDTO.Content;

            await _context.SaveChangesAsync();

            return report;

        }

        public async Task<bool> DeleteReportAsync(int reportId)
        {
            var report = await _context.Reports.FindAsync(reportId);

            if (report == null) return false;

            _context.Reports.Remove(report);

            await _context.SaveChangesAsync();

            return true;
        }

    }
}
