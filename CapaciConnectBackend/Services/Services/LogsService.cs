using CapaciConnectBackend.Context;
using CapaciConnectBackend.Models.Domain;
using CapaciConnectBackend.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace CapaciConnectBackend.Services.Services
{
    public class LogsService : ILogs
    {
        private readonly AplicationDBContext _context;
        private readonly IConfiguration _configuration;
        private readonly IError _errorService;
        public LogsService(AplicationDBContext context, IConfiguration configuration, IError errorService)
        {
            _context = context;
            _configuration = configuration;
            _errorService = errorService;
        }

        public async Task<List<Logs>> GetAllLogsAsync()
        {
            var logs = await _context.Logs.AsNoTracking().ToListAsync();

            return logs;
        }

        public async Task<List<Sessions>> GetAllSessionsAsync()
        {
            var logs = await _context.Sessions.AsNoTracking().ToListAsync();

            return logs;
        }
    }
}
