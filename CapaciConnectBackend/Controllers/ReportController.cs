using CapaciConnectBackend.DTOS;
using CapaciConnectBackend.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CapaciConnectBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReportController : ControllerBase
    {
        private readonly IReports _reportService;

        public ReportController(IReports reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("AllReports")]

        public async Task<IActionResult> GetAllReports()
        {
            var role = User.FindFirstValue(ClaimTypes.Role);

            if (role == "1")
            {
                var reports = await _reportService.GetAllReportsAsync();

                if (reports == null)
                {
                    return NotFound(new { message = "No reports found." });
                }

                return Ok(reports);
            }
            else
            {
                return Unauthorized(new { message = "User unauthorized.", role });
            }
        }

        [HttpGet("ReportsByWorkshopId/{workshopId}")]

        public async Task<IActionResult> GetReportsByWorkshop([FromRoute] int workshopId)
        {
            var role = User.FindFirstValue(ClaimTypes.Role);

            if (role == "1")
            {
                var reports = await _reportService.GetReportsByWorkshopIdAsync(workshopId);

                if (reports == null)
                {
                    return NotFound(new { message = "No reports found for this workshop." });
                }

                return Ok(reports);
            }
            else
            {
                return Unauthorized(new { message = "User Unauthorized", role });
            }
        }

        [HttpPost("CreateReport")]
        public async Task<IActionResult> CreateReport([FromBody] ReportDTO reportDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return NotFound(new { message = "UserNotFound" });
            }

            var report = await _reportService.CreateReportAsync(reportDTO, int.Parse(userId));

            if (report == null)
            {
                return BadRequest(new { message = "Report Title already exists." });
            }

            return Ok(report);
        }

        // Editar creo que no es nececario

        [HttpDelete("DeleteReport/{reportId}")]

        public async Task<IActionResult> DeleteReport([FromRoute] int reportId)
        {
            var role = User.FindFirstValue(ClaimTypes.Role);

            if (role == "1")
            {
                var report = await _reportService.DeleteReportAsync(reportId);

                if (!report)
                {
                    return NotFound(new { message = "report not found" });
                }

                return Ok(report);
            }
            else
            {
                return Unauthorized(new { message = "UserUnauthorized" });
            }

        }
    }
}
