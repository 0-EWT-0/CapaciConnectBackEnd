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
        private readonly IError _errorService;
        public ReportService(AplicationDBContext context, IConfiguration configuration, IError errorService)
        {
            _context = context;
            _configuration = configuration;
            _errorService = errorService;
        }

        public async Task<List<Reports>> GetAllReportsAsync()
        {
            try
            {
                var reports = await _context.Reports.AsNoTracking().ToListAsync();

                return reports;
            }
            catch (Exception ex)
            {
                var log = await _errorService.SaveErrorLogAsync($"Error in Get Reports: {ex.Message}");

                if (log == null)
                {
                    Console.WriteLine("Error log could not be saved.");
                }

                return new List<Reports>();
            }
        }

        public async Task<List<Reports>> GetReportsByWorkshopIdAsync(int workshopId)
        {
            try
            {
                var report = await _context.Reports.Where(r => r.Id_workshop_id == workshopId).AsNoTracking().ToListAsync();

                return report;
            }
            catch (Exception ex)
            {
                var log = await _errorService.SaveErrorLogAsync($"Error in Get Progressions: {ex.Message}");

                if (log == null)
                {
                    Console.WriteLine("Error log could not be saved.");
                }

                return new List<Reports>();
            }
        }

        public async Task<Reports?> CreateReportAsync(ReportDTO reportDTO, int userId)
        {
            try
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
            catch (Exception ex)
            {
                var log = await _errorService.SaveErrorLogAsync($"Error in Post Progressions: {ex.Message}");

                if (log == null)
                {
                    Console.WriteLine("Error log could not be saved.");
                }

                return null;
            }

        }

        public async Task<Reports?> UpdateReportAsync(int reportId, UpdateReportDTO reportDTO)
        {
            try
            {
                var report = await _context.Reports.FindAsync(reportId);

                if (report == null) return null;

                report.Tittle = reportDTO.Tittle;
                report.Content = reportDTO.Content;

                await _context.SaveChangesAsync();

                return report;

            }
            catch (Exception ex)
            {
                var log = await _errorService.SaveErrorLogAsync($"Error in Put Progressions: {ex.Message}");

                if (log == null)
                {
                    Console.WriteLine("Error log could not be saved.");
                }

                return null;
            }

        }

        public async Task<bool> DeleteReportAsync(int reportId)
        {
            try
            {
                var report = await _context.Reports.FindAsync(reportId);

                if (report == null) return false;

                _context.Reports.Remove(report);

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                var log = await _errorService.SaveErrorLogAsync($"Error in Put Progressions: {ex.Message}");

                if (log == null)
                {
                    Console.WriteLine("Error log could not be saved.");
                }

                return false;
            }
        }

    }
}
