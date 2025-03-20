using CapaciConnectBackend.Context;
using CapaciConnectBackend.DTOS;
using CapaciConnectBackend.Models.Domain;
using CapaciConnectBackend.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace CapaciConnectBackend.Services.Services
{
    public class WorkshopMultimediaService : IWorkshopMultimedia
    {
        private readonly AplicationDBContext _context;
        private readonly IConfiguration _configuration;
        private readonly IError _errorService;
        public WorkshopMultimediaService(AplicationDBContext context, IConfiguration configuration, IError errorService)
        {
            _context = context;
            _configuration = configuration;
            _errorService = errorService;
        }

        public async Task<List<WorkshopMultimedia>> GetAllWorkshopMultimedia()
        {
            try
            {
                var workshopMultimedia = await _context.WorkshopMultimedia.AsNoTracking().ToListAsync();

                return workshopMultimedia;
            }
            catch (Exception ex)
            {
                var log = await _errorService.SaveErrorLogAsync($"Error in Get WM: {ex.Message}");

                if (log == null)
                {
                    Console.WriteLine("Error log could not be saved.");
                }

                return new List<WorkshopMultimedia>();
            }
        }
        public async Task<List<WorkshopMultimedia>> GetAllWorkshoMultimediaByWorkshoId(int workshopId)
        {
            try
            {
                var workshopMultimedia = await _context.WorkshopMultimedia.Where(wm => wm.Id_workshop_id == workshopId).Include(wm => wm.Multimedia).AsNoTracking().ToListAsync();

                return workshopMultimedia;
            }
            catch (Exception ex)
            {
                var log = await _errorService.SaveErrorLogAsync($"Error in Get WM: {ex.Message}");

                if (log == null)
                {
                    Console.WriteLine("Error log could not be saved.");
                }

                return new List<WorkshopMultimedia>();
            }
        }

        public async Task<WorkshopMultimedia?> CreateWorkshopMultimedia(WorkshopMultimediaDTO workshopMultimediaDTO)
        {
            try
            {
                var workshop = await _context.Workshops.AsNoTracking()
.AnyAsync(w => w.Id_workshop == workshopMultimediaDTO.Id_workshop_id);

                var multimedia = await _context.Multimedia.AsNoTracking()
            .AnyAsync(m => m.Id_multimedia == workshopMultimediaDTO.Id_multimedia_id);


                if (!workshop || !multimedia)
                {
                    return null;
                }

                var existingRelation = await _context.WorkshopMultimedia
                    .AnyAsync(wm => wm.Id_workshop_id == workshopMultimediaDTO.Id_workshop_id
                                 && wm.Id_multimedia_id == workshopMultimediaDTO.Id_multimedia_id);

                if (existingRelation)
                {
                    return null;
                }


                var newWorkshopMultimedia = new WorkshopMultimedia
                {
                    Id_workshop_id = workshopMultimediaDTO.Id_workshop_id,
                    Id_multimedia_id = workshopMultimediaDTO.Id_multimedia_id
                };

                _context.WorkshopMultimedia.Add(newWorkshopMultimedia);
                await _context.SaveChangesAsync();

                return newWorkshopMultimedia;

            }
            catch (Exception ex)
            {
                var log = await _errorService.SaveErrorLogAsync($"Error in Put WM: {ex.Message}");

                if (log == null)
                {
                    Console.WriteLine("Error log could not be saved.");
                }

                return null;
            }
        }



        public async Task<bool> DeleteWorkshopMultimedia(int workshopId)
        {
            try
            {
                var workshopMultimedia = await _context.WorkshopMultimedia
                  .Where(wm => wm.Id_workshop_id == workshopId)
                  .ToListAsync();

                if (!workshopMultimedia.Any())
                {
                    return false;
                }

                _context.WorkshopMultimedia.RemoveRange(workshopMultimedia);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                var log = await _errorService.SaveErrorLogAsync($"Error in Delete WM: {ex.Message}");

                if (log == null)
                {
                    Console.WriteLine("Error log could not be saved.");
                }

                return false;
            }
        }
    }
}
