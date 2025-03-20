using CapaciConnectBackend.DTOS;
using CapaciConnectBackend.DTOS.Multimedia;
using CapaciConnectBackend.Models.Domain;

namespace CapaciConnectBackend.Services.IServices
{
    public interface IMultimedia
    {
        Task<List<Multimedia>> GetAllMultimediaAsync();

        Task<Multimedia?> CreateMultimediaAsync(MultimediaDTO multimediaDTO);

        Task<Multimedia?> UpdateMultimediaAsync(UpdateMultimediaDTO multimediaDTO, int multimediaId);

        Task<bool> DeleteMultimediaAsync(int multimediaId);

    }
}
