using CapaciConnectBackend.Context;
using CapaciConnectBackend.DTOS;
using CapaciConnectBackend.DTOS.Workshops;
using CapaciConnectBackend.Models.Domain;
using CapaciConnectBackend.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace CapaciConnectBackend.Services.Services
{
    public class WorkshopService : IWorkshop
    {
        private readonly AplicationDBContext _context;
        private readonly IConfiguration _configuration;
        public WorkshopService(AplicationDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<Workshops>> GetAllWorkshopsAsync()
        {
            var workshops = await _context.Workshops.AsNoTracking().ToListAsync();
            return workshops;
        }
        public async Task<Workshops?> CreateWorkshopAsync(WorkshopDTO workshopDTO, int userId)
        {
            var exists = await _context.Workshops.AnyAsync(w => w.Title == workshopDTO.Title);

            if (exists) return null;

            var newWorkshop = new Workshops
            {
                Title = workshopDTO.Title,
                Description = workshopDTO.Description,
                Content = workshopDTO.Content,
                Created_at = DateTime.Now,
                Id_user_id = userId,
                Id_type_id = workshopDTO.Id_type_id,
            };

            _context.Workshops.Add(newWorkshop);
            await _context.SaveChangesAsync();

            return newWorkshop;
        }

        public async Task<Workshops?> UpdateWorkshopAsync(int workshopId, UpdateWorkshopDTO workshopDTO)
        {
            var workshop = await _context.Workshops.FindAsync(workshopId);

            if (workshop == null) return null;

            workshop.Title = workshopDTO.Title;
            workshop.Description = workshopDTO.Description;
            workshop.Content = workshopDTO.Content;
            workshop.Id_type_id = workshopDTO.Id_type_id;

            await _context.SaveChangesAsync();

            return workshop;
        }

        public async Task<bool> DeleteWorkshopByIdAsync(int workshopId)
        {
            var workshop = await _context.Workshops.FindAsync(workshopId);

            if (workshop == null) return false;

            _context.Workshops.Remove(workshop);

            await _context.SaveChangesAsync();

            return true;
        }

    }
}
