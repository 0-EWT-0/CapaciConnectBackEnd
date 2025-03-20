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
        public WorkshopMultimediaService(AplicationDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<WorkshopMultimedia>> GetAllWorkshopMultimedia()
        {
            var workshopMultimedia = await _context.WorkshopMultimedia.AsNoTracking().ToListAsync();

            return workshopMultimedia;
        }
        public async Task<List<WorkshopMultimedia>> GetAllWorkshoMultimediaByWorkshoId(int workshopId)
        {
            var workshopMultimedia = await _context.WorkshopMultimedia.Where(wm => wm.Id_workshop_id == workshopId).Include(wm => wm.Multimedia).AsNoTracking().ToListAsync();

            return workshopMultimedia;
        }

        public async Task<WorkshopMultimedia?> CreateWorkshopMultimedia(WorkshopMultimediaDTO workshopMultimediaDTO)
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



        public async Task<bool> DeleteWorkshopMultimedia(int workshopId)
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
    }
}
