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
    public class WorkshopMultimediaController : ControllerBase
    {
        private readonly IWorkshopMultimedia _workshopMultimediaService;

        public WorkshopMultimediaController(IWorkshopMultimedia workshopMultimedia)
        {
            _workshopMultimediaService = workshopMultimedia;
        }

        [HttpGet("AllWorkshopMultimedia")]

        public async Task<IActionResult> GetAllWorkshopsMultimedia()
        {
            var role = User.FindFirstValue(ClaimTypes.Role);

            if (role == "1")
            {
                var workshopMultimedia = await _workshopMultimediaService.GetAllWorkshopMultimedia();

                if (workshopMultimedia == null)
                {
                    return NotFound(new { message = "workshopMultimedia not found" });
                }

                return Ok(workshopMultimedia);
            }
            else
            {
                return Unauthorized(new { message = "User Unauthorized", role });
            }
        }

        [HttpGet("GetWorkshopMultimediaByWorkshopId/{workshopId}")]

        public async Task<IActionResult> GetWorkshopMultimediaByWorkshopId([FromRoute] int workshopId)
        {
            var workshopMultiemdia = await _workshopMultimediaService.GetAllWorkshoMultimediaByWorkshoId(workshopId);

            if (workshopMultiemdia == null)
            {
                return NotFound(new { message = "No Multimedia found for this workshop" });
            }

            return Ok(workshopMultiemdia);
        }

        [HttpPost("CreateWorkshopMultimedia")]
        public async Task<IActionResult> CreateWorkshopMultimedia([FromBody] WorkshopMultimediaDTO workshopMultimediaDTO)
        {
            var role = User.FindFirstValue(ClaimTypes.Role);

            if (role == "1" || role == "2" || role == "3" || role == "5")
            {
                var workshopMultimedia = await _workshopMultimediaService.CreateWorkshopMultimedia(workshopMultimediaDTO);

                if (workshopMultimedia == null)
                {
                    return NotFound(new { message = "Workshop or Multimedia not found, or the relation already exists." });
                }

                return CreatedAtAction(nameof(CreateWorkshopMultimedia), new { id = workshopMultimedia.Id_workshop_id }, workshopMultimedia);
            }
            else
            {
                return Unauthorized(new { message = "User unauthorized.", role });
            }
        }

        [HttpDelete("DeleteWorkshopMultimedia/{workshopId}")]

        public async Task<IActionResult> DeleteWorkshopMultimedia([FromRoute] int workshopId)
        {
            var role = User.FindFirstValue(ClaimTypes.Role);

            if (role == "1" || role == "2" || role == "3" || role == "5")
            {
                var workshopMultimedia = await _workshopMultimediaService.DeleteWorkshopMultimedia(workshopId);

                if (!workshopMultimedia)
                {
                    return NotFound(new { message = "WorkshopMultimedia not found." });
                }

                return Ok(workshopMultimedia);
            }
            else
            {
                return Unauthorized(new { message = "UserUnauthorized" });
            }
        }

    }
}
