using CapaciConnectBackend.DTOS;
using CapaciConnectBackend.DTOS.Multimedia;
using CapaciConnectBackend.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CapaciConnectBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MultimediaController : ControllerBase
    {
        private readonly IMultimedia _multimediaService;
        public MultimediaController(IMultimedia multimedia)
        {
            _multimediaService = multimedia;
        }

        [HttpGet("AllMultimedia")]

        public async Task<IActionResult> GetAllMultimedia()
        {
            var role = User.FindFirstValue(ClaimTypes.Role);

            if (role == "1")
            {
                var multimedia = await _multimediaService.GetAllMultimediaAsync();
                return Ok(multimedia);
            }
            else
            {
                return Unauthorized(new { message = "User Unauthorized", role });
            }
        }

        [HttpPost("CreateMultimedia")]

        public async Task<IActionResult> CreateMultimedia([FromBody] MultimediaDTO multimediaDTO)
        {
            if (multimediaDTO == null || string.IsNullOrEmpty(multimediaDTO.Media_url))
            {
                return BadRequest(new { message = "Invalid multimedia data." });
            }

            if (multimediaDTO == null)
            {
                return BadRequest(new { message = "Invalid multimedia data." });
            }

            var role = User.FindFirstValue(ClaimTypes.Role);

            if (role == "1" || role == "2" || role == "3")
            {
                var createdMultimedia = await _multimediaService.CreateMultimediaAsync(multimediaDTO);

                if (createdMultimedia == null)
                {
                    return BadRequest(new { message = "Failed to create multimedia." });
                }

                return Ok(createdMultimedia);
            }
            else
            {
                return Unauthorized(new { message = "User Unauthorized", role });
            }
        }

        [HttpPut("UpdateMultimedia/{multimediaId}")]

        public async Task<IActionResult> UpdateMultimedia([FromRoute] int multimediaId, [FromBody] UpdateMultimediaDTO multimediaDTO)
        {
            var role = User.FindFirstValue(ClaimTypes.Role);

            if (role == "1" || role == "2" || role == "3")
            {
                var updatedMultimedia = await _multimediaService.UpdateMultimediaAsync(multimediaDTO, multimediaId);

                if (updatedMultimedia == null)
                {
                    return NotFound(new { message = "Multimedia not found" });
                }

                return Ok(updatedMultimedia);
            }
            else
            {
                return Unauthorized(new { message = "User Unauthorized", role });
            }
        }

        [HttpDelete("DeleteMultimedia/{multimediaId}")]

        public async Task<IActionResult> DeleteMultimedia(int multimediaId)
        {
            var role = User.FindFirstValue(ClaimTypes.Role);

            if (role == "1" || role == "2" || role == "3")
            {
                var deletedMultimedia = await _multimediaService.DeleteMultimediaAsync(multimediaId);

                if (!deletedMultimedia)
                {
                    return NotFound(new { message = "Multimedia not found." });
                }

                return Ok(deletedMultimedia);
            }
            else
            {
                return Unauthorized(new { message = "User Unauthorized", role });
            }
        }
    }
}
