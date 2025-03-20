using CapaciConnectBackend.Context;
using CapaciConnectBackend.DTOS;
using CapaciConnectBackend.DTOS.Progressions;
using CapaciConnectBackend.Models.Domain;
using CapaciConnectBackend.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace CapaciConnectBackend.Services.Services
{
    public class ProgressionService : IProgressions
    {
        private readonly AplicationDBContext _context;
        private readonly IConfiguration _configuration;
        private readonly IError _errorService;
        public ProgressionService(AplicationDBContext context, IConfiguration configuration, IError errorService)
        {
            _context = context;
            _configuration = configuration;
            _errorService = errorService;
        }

        public async Task<List<Progressions>> GetAllProgressionsAsync()
        {
            try
            {
                var progressions = await _context.Progressions.AsNoTracking().ToListAsync();

                return progressions;
            }
            catch (Exception ex)
            {
                var log = await _errorService.SaveErrorLogAsync($"Error in Get Progressions: {ex.Message}");

                if (log == null)
                {
                    Console.WriteLine("Error log could not be saved.");
                }

                return new List<Progressions>();
            }
        }

        public async Task<List<Progressions>> GetAllProgressionsByWorkshopIdAsync(int workshopId)
        {
            try
            {
                var progressions = await _context.Progressions.Where(p => p.Id_workshop_id == workshopId).AsNoTracking().ToListAsync();

                return progressions;
            }
            catch (Exception ex)
            {
                var log = await _errorService.SaveErrorLogAsync($"Error in Get Progressions: {ex.Message}");

                if (log == null)
                {
                    Console.WriteLine("Error log could not be saved.");
                }

                return new List<Progressions>();
            }
        }

        public async Task<List<Progressions>> GetProgressionsByUserIdAsync(int userId)
        {
            try
            {
                var progressions = await _context.Progressions.Where(p => p.Id_user_id == userId).AsNoTracking().ToListAsync();

                return progressions;
            }
            catch (Exception ex)
            {
                var log = await _errorService.SaveErrorLogAsync($"Error in Get Progressions: {ex.Message}");

                if (log == null)
                {
                    Console.WriteLine("Error log could not be saved.");
                }

                return new List<Progressions>();
            }
        }

        public async Task<Progressions?> CreateProgressionsAsync(ProgressionDTO progressionDTO, int userId)
        {
            try
            {
                var workshopExists = await _context.Workshops.AnyAsync(w => w.Id_workshop == progressionDTO.Id_workshop_id);

                if (!workshopExists) return null;

                var exist = await _context.Progressions.AnyAsync(p => p.Id_user_id == userId && p.Id_workshop_id == progressionDTO.Id_workshop_id);

                if (exist) return null;

                var newProgression = new Progressions
                {
                    Progression_status = progressionDTO.Progression_status,
                    Id_user_id = userId,
                    Id_workshop_id = progressionDTO.Id_workshop_id,
                };

                _context.Progressions.Add(newProgression);

                await _context.SaveChangesAsync();

                return newProgression;
            }
            catch (Exception ex)
            {
                var log = await _errorService.SaveErrorLogAsync($"Error in Post Progressions: {ex.Message}");

                if (log == null)
                {
                    Console.WriteLine("Error log could not be saved.");
                }

                return null;
            }
        }

        public async Task<Progressions?> UpdateProgressionsAsync(UpdateProgressionDTO updateProgressionDTO, int progressionId)
        {
            try
            {
                var progression = await _context.Progressions.FindAsync(progressionId);

                if (progression == null) return null;

                progression.Progression_status = updateProgressionDTO.Progression_status;

                await _context.SaveChangesAsync();

                return progression;
            }
            catch (Exception ex)
            {
                var log = await _errorService.SaveErrorLogAsync($"Error in Put Progressions: {ex.Message}");

                if (log == null)
                {
                    Console.WriteLine("Error log could not be saved.");
                }

                return null;
            }
        }

        public async Task<bool> DeleteAllUserProgressions(int userId)
        {
            try
            {
                var progressions = await _context.Progressions.Where(p => p.Id_user_id == userId).ToListAsync();

                if (!progressions.Any()) return false;

                _context.Progressions.RemoveRange(progressions);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                var log = await _errorService.SaveErrorLogAsync($"Error in Delete Progressions: {ex.Message}");

                if (log == null)
                {
                    Console.WriteLine("Error log could not be saved.");
                }

                return false;
            }
        }

        public async Task<bool> DeleteProgressionsAsync(int progressionId)
        {
            try
            {
                var progression = await _context.Progressions.FindAsync(progressionId);

                if (progression == null) return false;

                _context.Progressions.Remove(progression);

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                var log = await _errorService.SaveErrorLogAsync($"Error in Delete Progressions: {ex.Message}");

                if (log == null)
                {
                    Console.WriteLine("Error log could not be saved.");
                }

                return false;
            }
        }
    }
}
