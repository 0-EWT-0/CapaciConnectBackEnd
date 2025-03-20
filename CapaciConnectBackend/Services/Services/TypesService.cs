using CapaciConnectBackend.Context;
using CapaciConnectBackend.DTOS;
using CapaciConnectBackend.Models.Domain;
using CapaciConnectBackend.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace CapaciConnectBackend.Services.Services
{
    public class TypesService : ITypes
    {

        private readonly AplicationDBContext _context;
        private readonly IConfiguration _configuration;
        private readonly IError _errorService;
        public TypesService(AplicationDBContext context, IConfiguration configuration, IError errorService)
        {
            _context = context;
            _configuration = configuration;
            _errorService = errorService;
        }
        public async Task<List<WorkshopTypes>> GetAllTypesAsync()
        {
            try
            {
                var types = await _context.WorkshopTypes.AsNoTracking().ToListAsync();

                return types;
            }
            catch (Exception ex)
            {
                var log = await _errorService.SaveErrorLogAsync($"Error in Get Types: {ex.Message}");

                if (log == null)
                {
                    Console.WriteLine("Error log could not be saved.");
                }

                return new List<WorkshopTypes>();
            }
        }

        public async Task<WorkshopTypes?> GetWorkshopTypeById(int typeId)
        {
            try
            {
                var type = await _context.WorkshopTypes.FindAsync(typeId);

                return type;
            }
            catch (Exception ex)
            {
                var log = await _errorService.SaveErrorLogAsync($"Error in Get Types: {ex.Message}");

                if (log == null)
                {
                    Console.WriteLine("Error log could not be saved.");
                }

                return null;
            }
        }

        public async Task<WorkshopTypes?> CreateWorkshopTypeAsync(WorkshopTypeDTO workshopTypeDTO)
        {
            try
            {
                var exists = await _context.WorkshopTypes.AnyAsync(w => w.Type_name == workshopTypeDTO.Type_name);

                if (exists) return null;

                var newType = new WorkshopTypes
                {
                    Type_name = workshopTypeDTO.Type_name,
                };

                _context.WorkshopTypes.Add(newType);
                await _context.SaveChangesAsync();

                return newType;

            }
            catch (Exception ex)
            {
                var log = await _errorService.SaveErrorLogAsync($"Error in Post Types: {ex.Message}");

                if (log == null)
                {
                    Console.WriteLine("Error log could not be saved.");
                }

                return null;
            }
        }

        public async Task<bool> DeleteWorkshopTypeById(int typeId)
        {
            try
            {
                var type = _context.WorkshopTypes.Find(typeId);

                if (type == null) return false;

                _context.WorkshopTypes.Remove(type);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                var log = await _errorService.SaveErrorLogAsync($"Error in Delte Types: {ex.Message}");

                if (log == null)
                {
                    Console.WriteLine("Error log could not be saved.");
                }

                return false;
            }
        }


    }
}
