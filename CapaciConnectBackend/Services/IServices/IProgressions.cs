using CapaciConnectBackend.DTOS;
using CapaciConnectBackend.DTOS.Progressions;
using CapaciConnectBackend.Models.Domain;

namespace CapaciConnectBackend.Services.IServices
{
    public interface IProgressions
    {
        Task<List<Progressions>> GetAllProgressionsAsync();
        Task<List<Progressions>> GetProgressionsByUserIdAsync(int userId);
        Task<List<Progressions>> GetAllProgressionsByWorkshopIdAsync(int workshopId);
        Task<Progressions?> CreateProgressionsAsync(ProgressionDTO progressionDTO, int userId);
        Task<Progressions?> UpdateProgressionsAsync(UpdateProgressionDTO updateProgressionDTO, int progressionId);
        Task<bool> DeleteProgressionsAsync(int progressionId);
        Task<bool> DeleteAllUserProgressions(int userId);

    }
}
