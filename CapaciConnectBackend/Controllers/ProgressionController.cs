using CapaciConnectBackend.DTOS;
using CapaciConnectBackend.DTOS.Progressions;
using CapaciConnectBackend.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CapaciConnectBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProgressionController : ControllerBase
    {
        private readonly IProgressions _progressionService;

        public ProgressionController(IProgressions progressionService)
        {
            _progressionService = progressionService;
        }

        [HttpGet("AllProgressions")]

        public async Task<IActionResult> GetAllProgressions()
        {
            var role = User.FindFirstValue(ClaimTypes.Role);

            if (role == "1")
            {
                var progressions = await _progressionService.GetAllProgressionsAsync();

                if (progressions == null)
                {
                    return NotFound(new { message = "No progressions found." });
                }

                return Ok(progressions);
            }
            else
            {
                return Unauthorized(new { message = "User unauthorized.", role });
            }
        }

        [HttpGet("ProgressionsByWorkshopId/{workshopId}")]

        public async Task<IActionResult> GetProgressionByWorkshop([FromRoute] int workshopId)
        {
            var role = User.FindFirstValue(ClaimTypes.Role);

            if (role == "1")
            {
                var progressions = await _progressionService.GetAllProgressionsByWorkshopIdAsync(workshopId);

                if (progressions == null)
                {
                    return NotFound(new { message = "No progressions found for this workshop." });
                }

                return Ok(progressions);
            }
            else
            {
                return Unauthorized(new { message = "User unauthorized.", role });
            }
        }

        [HttpGet("ProgressionsByUserId")]

        public async Task<IActionResult> GetProgressionByUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return NotFound(new { message = "User not found." });
            }

            var progressions = await _progressionService.GetProgressionsByUserIdAsync(int.Parse(userId));

            return Ok(progressions);
        }

        [HttpPost("CreateProgression")]

        public async Task<IActionResult> CreateProgression([FromBody] ProgressionDTO progressionDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return NotFound(new { message = "User not found." });
            }

            var newProgression = await _progressionService.CreateProgressionsAsync(progressionDTO, int.Parse(userId));

            if (newProgression == null)
            {
                return BadRequest(new { message = "Progression already exists." });
            }

            return Ok(newProgression);
        }

        [HttpPut("UpdateProgression/{progressionId}")]

        public async Task<IActionResult> UpdateProgression([FromRoute] int progressionId, [FromBody] UpdateProgressionDTO updateProgressionDTO)
        {

            var updatedProgression = await _progressionService.UpdateProgressionsAsync(updateProgressionDTO, progressionId);

            if (updatedProgression == null)
            {
                return NotFound(new { message = "Progression not found." });
            }

            return Ok(updatedProgression);
        }

        [HttpDelete("DeleteProgression/{progressionId}")]

        public async Task<IActionResult> DeleteProgression([FromRoute] int progressionId)
        {
            var progression = await _progressionService.DeleteProgressionsAsync(progressionId);

            if (!progression)
            {
                return NotFound(new { message = "Progression not found." });
            }

            return Ok(progression);
        }

        [HttpDelete("DeleteAllUserProgressions/{userId}")]

        public async Task<IActionResult> DeleteAllUserProgressions([FromRoute] int userId)
        {
            var role = User.FindFirstValue(ClaimTypes.Role);

            if (role == "1")
            {
                var progressions = await _progressionService.DeleteAllUserProgressions(userId);

                if (!progressions)
                {
                    return NotFound(new { message = "Progressions not found." });
                }

                return Ok(progressions);
            }
            else
            {
                return Unauthorized(new { message = "User unauthorized.", role });
            }
        }
    }
}

