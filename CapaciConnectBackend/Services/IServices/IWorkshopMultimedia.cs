using CapaciConnectBackend.DTOS;
using CapaciConnectBackend.Models.Domain;

namespace CapaciConnectBackend.Services.IServices
{
    public interface IWorkshopMultimedia
    {
        Task<List<WorkshopMultimedia>> GetAllWorkshopMultimedia();
        Task<List<WorkshopMultimedia>> GetAllWorkshoMultimediaByWorkshoId(int workshopId);
        Task<WorkshopMultimedia?> CreateWorkshopMultimedia(WorkshopMultimediaDTO workshopMultimediaDTO);
        Task<bool> DeleteWorkshopMultimedia(int workshopID);

    }
}
