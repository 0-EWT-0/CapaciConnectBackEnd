using CapaciConnectBackend.DTOS;
using CapaciConnectBackend.DTOS.Workshops;
using CapaciConnectBackend.Models.Domain;

namespace CapaciConnectBackend.Services.IServices
{
    public interface IWorkshop
    {
        Task<List<Workshops>> GetAllWorkshopsAsync();
        Task<Workshops?> CreateWorkshopAsync(WorkshopDTO workshopDTO, int userId);
        Task<Workshops?> UpdateWorkshopAsync(int workshopId, UpdateWorkshopDTO workshopDTO);
        Task<bool> DeleteWorkshopByIdAsync(int workshopId);
    }
}
