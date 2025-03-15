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
        public TypesService(AplicationDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<List<WorkshopTypes>> GetAllTypesAsync()
        {
            var types = await _context.WorkshopTypes.AsNoTracking().ToListAsync();

            return types;
        }

        public async Task<WorkshopTypes?> GetWorkshopTypeById(int typeId)
        {
            var type = await _context.WorkshopTypes.FindAsync(typeId);

            return type;
        }

        public async Task<WorkshopTypes?> CreateWorkshopTypeAsync(WorkshopTypeDTO workshopTypeDTO)
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

        public async Task<bool> DeleteWorkshopTypeById(int typeId)
        {
            var type = _context.WorkshopTypes.Find(typeId);

            if (type == null) return false;

            _context.WorkshopTypes.Remove(type);
            await _context.SaveChangesAsync();

            return true;
        }


    }
}
