using CapaciConnectBackend.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            var role = User.FindFirstValue(ClaimTypes.Role);

            if (role == "1")
            {
                var logs = await _logsService.GetAllLogsAsync();
                return Ok(logs);
            }
            else
            {
                return Unauthorized(new { message = "User unauthorized.", role });
            }
        }

        [HttpGet("AllSessions")]

        public async Task<IActionResult> GetAllSessions()
        {
            var role = User.FindFirstValue(ClaimTypes.Role);

            if (role == "1")
            {
                var sessions = await _logsService.GetAllSessionsAsync();
                return Ok(sessions);
            }
            else
            {
                return Unauthorized(new { message = "User unauthorized.", role });
            }
        }
    }
}
