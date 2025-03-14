using CapaciConnectBackend.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CapaciConnectBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LogController : ControllerBase
    {
        private readonly ILogs _logsService;

        public LogController(ILogs logsService)
        {
            _logsService = logsService;
        }

        [HttpGet("AllLogs")]

        public async Task<IActionResult> GetAllLogs()
        {
            var logs = await _logsService.GetAllLogsAsync();
            return Ok(logs);
        }

        [HttpGet("AllSessions")]

        public async Task<IActionResult> GetAllSessions()
        {
            var sessions = await _logsService.GetAllSessionsAsync();
            return Ok(sessions);
        }
    }
}
