using CapaciConnectBackend.Context;
using CapaciConnectBackend.Models.Domain;
using CapaciConnectBackend.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace CapaciConnectBackend.Services.Services
{
    public class ErrorService : IError
    {
        private readonly AplicationDBContext _context;
        private readonly IConfiguration _configuration;
        public ErrorService(AplicationDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<Logs?> SaveErrorLogAsync(string errorMessage)
        {
            try
            {
                var recentError = await _context.Logs.OrderByDescending(l => l.Created_at).FirstOrDefaultAsync();

                if (recentError == null || recentError.Content != errorMessage || (DateTime.Now - recentError.Created_at).TotalMinutes > 5)
                {
                    var newLog = new Logs
                    {
                        Content = errorMessage,
                        Created_at = DateTime.Now
                    };

                    _context.Logs.Add(newLog);
                    await _context.SaveChangesAsync();

                    return newLog;
                }
                return recentError;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to log error: {ex.Message}");
                return null;
            }
        }
    }
}
