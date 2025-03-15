using CapaciConnectBackend.DTOS;
using CapaciConnectBackend.Models.Domain;

namespace CapaciConnectBackend.Services.IServices
{
    public interface ITypes
    {
        Task<List<WorkshopTypes>> GetAllTypesAsync(); 
        Task<WorkshopTypes?> GetWorkshopTypeById(int typeId);
        Task<WorkshopTypes?> CreateWorkshopTypeAsync(WorkshopTypeDTO workshopTypeDTO);
        Task<bool> DeleteWorkshopTypeById(int typeId);
    }
}
