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
        private readonly IError _errorService;
        public WorkshopService(AplicationDBContext context, IConfiguration configuration, IError errroService)
        {
            _context = context;
            _configuration = configuration;
            _errorService = errroService;
        }

        public async Task<List<Workshops>> GetAllWorkshopsAsync()
        {
            try
            {
                var workshops = await _context.Workshops.AsNoTracking().ToListAsync();
                return workshops;
            }
            catch (Exception ex)
            {
                var log = await _errorService.SaveErrorLogAsync($"Error in Get Workshops: {ex.Message}");

                if (log == null)
                {
                    Console.WriteLine("Error log could not be saved.");
                }

                return new List<Workshops>();
            }
        }
        public async Task<Workshops?> CreateWorkshopAsync(WorkshopDTO workshopDTO, int userId)
        {
            try
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
            catch (Exception ex)
            {
                var log = await _errorService.SaveErrorLogAsync($"Error in Post Workshops: {ex.Message}");

                if (log == null)
                {
                    Console.WriteLine("Error log could not be saved.");
                }

                return null;
            }
        }

        public async Task<Workshops?> UpdateWorkshopAsync(int workshopId, UpdateWorkshopDTO workshopDTO)
        {
            try
            {
                var workshop = await _context.Workshops.FindAsync(workshopId);

                if (workshop == null) return null;

                workshop.Title = workshopDTO.Title;
                workshop.Description = workshopDTO.Description;
                workshop.Content = workshopDTO.Content;
                workshop.Image = workshopDTO.Image;
                workshop.Id_type_id = workshopDTO.Id_type_id;

                await _context.SaveChangesAsync();

                return workshop;
            }
            catch (Exception ex)
            {
                var log = await _errorService.SaveErrorLogAsync($"Error in Put Workshops: {ex.Message}");

                if (log == null)
                {
                    Console.WriteLine("Error log could not be saved.");
                }

                return null;
            }
        }

        public async Task<bool> DeleteWorkshopByIdAsync(int workshopId)
        {
            try
            {
                var workshop = await _context.Workshops.FindAsync(workshopId);

                if (workshop == null) return false;

                _context.Workshops.Remove(workshop);

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                var log = await _errorService.SaveErrorLogAsync($"Error in Delete Workshops: {ex.Message}");

                if (log == null)
                {
                    Console.WriteLine("Error log could not be saved.");
                }

                return false;
            }
        }

    }
}
