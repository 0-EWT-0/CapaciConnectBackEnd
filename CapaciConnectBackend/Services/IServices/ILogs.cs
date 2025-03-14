using CapaciConnectBackend.Models.Domain;

namespace CapaciConnectBackend.Services.IServices
{
    public interface ILogs
    {
        Task<List<Logs>> GetAllLogsAsync();
        Task<List<Sessions>> GetAllSessionsAsync();
    }
}
