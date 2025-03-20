using CapaciConnectBackend.Context;
using CapaciConnectBackend.DTOS;
using CapaciConnectBackend.DTOS.Multimedia;
using CapaciConnectBackend.Models.Domain;
using CapaciConnectBackend.Services.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace CapaciConnectBackend.Services.Services
{
    public class MultimediaService : IMultimedia
    {
        private readonly AplicationDBContext _context;
        private readonly IConfiguration _configuration;
        public MultimediaService(AplicationDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<Multimedia>> GetAllMultimediaAsync()
        {
            var multimedia = await _context.Multimedia.AsNoTracking().ToListAsync();

            return multimedia;
        }

        public async Task<Multimedia?> CreateMultimediaAsync(MultimediaDTO multimediaDTO)
        {
            if (string.IsNullOrEmpty(multimediaDTO.Media_url) || string.IsNullOrEmpty(multimediaDTO.Media_type))
            {
                return null;
            }

            if (!Enum.TryParse<MediaType>(multimediaDTO.Media_type, true, out var mediaType))
            {
                return null;
            }

            var newMultimedia = new Multimedia
            {
                Media_type = mediaType,
                Media_url = multimediaDTO.Media_url
            };

            _context.Multimedia.Add(newMultimedia);
            await _context.SaveChangesAsync();

            return newMultimedia;
        }


        public async Task<Multimedia?> UpdateMultimediaAsync(UpdateMultimediaDTO multimediaDTO, int multimediaId)
        {
            var multimedia = await _context.Multimedia.FindAsync(multimediaId);

            if (multimedia == null) return null;

            multimedia.Media_url = multimediaDTO.Media_url;

            await _context.SaveChangesAsync();

            return multimedia;
        }

        public async Task<bool> DeleteMultimediaAsync(int multimediaId)
        {
            var multimedia = await _context.Multimedia.Include(wm => wm.WorkshopMultimedia).FirstOrDefaultAsync(m => m.Id_multimedia == multimediaId);

            if (multimedia == null) return false;

            if (multimedia.WorkshopMultimedia.Any())
            {
                _context.WorkshopMultimedia.RemoveRange(multimedia.WorkshopMultimedia);
            }

            _context.Multimedia.Remove(multimedia);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
