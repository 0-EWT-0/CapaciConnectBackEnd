using CapaciConnectBackend.DTOS;
using CapaciConnectBackend.DTOS.Reports;
using CapaciConnectBackend.Models.Domain;

namespace CapaciConnectBackend.Services.IServices
{
    public interface IReports
    {
        Task<List<Reports>> GetAllReportsAsync();
        Task<List<Reports>> GetReportsByWorkshopIdAsync(int workshopId);
        Task<Reports?> CreateReportAsync(ReportDTO resportDTO, int userId);
        Task<Reports?> UpdateReportAsync(int reportId, UpdateReportDTO reportDTO);
        Task<bool> DeleteReportAsync(int reportId);
    }
}
