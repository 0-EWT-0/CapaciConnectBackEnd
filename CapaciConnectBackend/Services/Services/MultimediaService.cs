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
        private readonly IError _errorService;
        public MultimediaService(AplicationDBContext context, IConfiguration configuration, IError errorService)
        {
            _context = context;
            _configuration = configuration;
            _errorService = errorService;
        }

        public async Task<List<Multimedia>> GetAllMultimediaAsync()
        {
            try
            {
                var multimedia = await _context.Multimedia.AsNoTracking().ToListAsync();

                return multimedia;
            }
            catch (Exception ex)
            {
                var log = await _errorService.SaveErrorLogAsync($"Error in Get Multimedia: {ex.Message}");

                if (log == null)
                {
                    Console.WriteLine("Error log could not be saved.");
                }

                return new List<Multimedia>();
            }
        }

        public async Task<Multimedia?> CreateMultimediaAsync(MultimediaDTO multimediaDTO)
        {
            try
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
            catch (Exception ex)
            {
                var log = await _errorService.SaveErrorLogAsync($"Error in Post Multimedia: {ex.Message}");

                if (log == null)
                {
                    Console.WriteLine("Error log could not be saved.");
                }

                return null;
            }

        }


        public async Task<Multimedia?> UpdateMultimediaAsync(UpdateMultimediaDTO multimediaDTO, int multimediaId)
        {
            try
            {
                var multimedia = await _context.Multimedia.FindAsync(multimediaId);

                if (multimedia == null) return null;

                multimedia.Media_url = multimediaDTO.Media_url;

                await _context.SaveChangesAsync();

                return multimedia;
            }
            catch (Exception ex)
            {
                var log = await _errorService.SaveErrorLogAsync($"Error in Put Multimedia: {ex.Message}");

                if (log == null)
                {
                    Console.WriteLine("Error log could not be saved.");
                }

                return null;
            }
        }

        public async Task<bool> DeleteMultimediaAsync(int multimediaId)
        {
            try
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
            catch (Exception ex)
            {
                var log = await _errorService.SaveErrorLogAsync($"Error in Delete Multimedia: {ex.Message}");

                if (log == null)
                {
                    Console.WriteLine("Error log could not be saved.");
                }

                return false;
            }
        }
    }
}
