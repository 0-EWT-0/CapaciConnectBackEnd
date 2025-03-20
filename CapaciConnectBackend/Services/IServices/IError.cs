using CapaciConnectBackend.Models.Domain;

namespace CapaciConnectBackend.Services.IServices
{
    public interface IError
    {
        Task<Logs?> SaveErrorLogAsync(string errorMessage);
    }
}
