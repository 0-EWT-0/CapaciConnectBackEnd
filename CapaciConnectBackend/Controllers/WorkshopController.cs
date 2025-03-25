using CapaciConnectBackend.DTOS;
using CapaciConnectBackend.DTOS.Workshops;
using CapaciConnectBackend.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CapaciConnectBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WorkshopController : ControllerBase
    {
        private readonly IWorkshop _workshopService;

        public WorkshopController(IWorkshop workshop)
        {
            _workshopService = workshop;
        }

        [HttpGet("AllWorkshops")]

        public async Task<IActionResult> GetAllWorkshops()
        {
            var workshops = await _workshopService.GetAllWorkshopsAsync();
            return Ok(workshops);
        }

        [HttpPost("CreateWorkshop")]

        public async Task<IActionResult> CreateWorkshop([FromBody] WorkshopDTO workshopDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var role = User.FindFirstValue(ClaimTypes.Role);

            if (userId == null)
            {
                return Unauthorized(new { message = "User unauthorized.", role });
            }

            if (role == "1" || role == "2")
            {
                var createdWorkshop = await _workshopService.CreateWorkshopAsync(workshopDTO, int.Parse(userId));

                if (createdWorkshop == null)
                {
                    return BadRequest(new { message = "Workshop already exists. Or type id dosent exists" });
                }

                return Ok(createdWorkshop);
            }
            else
            {
                return Unauthorized(new { message = "User unauthorized.", role });
            }

        }
        [HttpPut("UpdateWorkshop/{workshopId}")]

        public async Task<IActionResult> UpdateWorkshop([FromRoute] int workshopId, [FromBody] UpdateWorkshopDTO workshopDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var role = User.FindFirstValue(ClaimTypes.Role);

            if (userId == null)
            {
                return Unauthorized(new { message = "User unauthorized.", role });
            }

            if (role == "1" || role == "2" || role == "3")        
            {
                var updatedWorkshop = await _workshopService.UpdateWorkshopAsync(workshopId, workshopDTO);

                if (updatedWorkshop == null)
                {
                    return BadRequest(new { message = "Workshop not found." });
                }

                return Ok(updatedWorkshop);
            }
            else
            {
                return Unauthorized(new { message = "User unauthorized.", role });
            }

        }

        [HttpDelete("DeleteWorkshop/{workshopId}")]

        public async Task<IActionResult> DeleteWorkshop([FromRoute] int workshopId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var role = User.FindFirstValue(ClaimTypes.Role);

            if (userId == null)
            {
                return Unauthorized(new { message = "User not found or unauthorized.", role });
            }

            if (role == "1" || role == "2")
            {
                var deletedWorkshop = await _workshopService.DeleteWorkshopByIdAsync(workshopId);

                if (!deletedWorkshop)
                {
                    return NotFound(new { message = "Workshop not found." });
                }

                return Ok(deletedWorkshop);
            }
            else
            {
                return Unauthorized(new { message = "User unauthorized.", role });
            }

        }

    }
}
