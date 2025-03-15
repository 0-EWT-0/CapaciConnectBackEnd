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
    public class TypeController : ControllerBase
    {
        private readonly ITypes _typeService;

        public TypeController(ITypes typeService)
        {
            _typeService = typeService;
        }

        [HttpGet("AllTypes")]

        public async Task<IActionResult> GetAllTypes()
        {
            var types = await _typeService.GetAllTypesAsync();
            return Ok(types);
        }

        [HttpGet("TypeById/{typeId}")]

        public async Task<IActionResult> GetTypeById([FromRoute] int typeId)
        {
            var type = await _typeService.GetWorkshopTypeById(typeId);

            if (type == null)
            {
                return NotFound(new { message = "Type not found." });
            }

            return Ok(type);
        }

        [HttpPost("CreateType")]

        public async Task<IActionResult> CreateType([FromBody] WorkshopTypeDTO typeDTO)
        {
            var role = User.FindFirstValue(ClaimTypes.Role);

            if (role == "1")
            {
                var createdType = await _typeService.CreateWorkshopTypeAsync(typeDTO);

                if (createdType == null)
                {
                    return BadRequest(new { message = "Type already exists." });
                }

                return Ok(createdType);
            }
            else
            {
                return Unauthorized(new { message = "User unauthorized.", role });
            }

        }

        [HttpDelete("DeleteType/{typeId}")]

        public async Task<IActionResult> DeleteType([FromRoute] int typeId)
        {
            var role = User.FindFirstValue(ClaimTypes.Role);

            if (role == "1")
            {
                var deleted = await _typeService.DeleteWorkshopTypeById(typeId);

                if (!deleted)
                {
                    return NotFound(new { message = "Type not found." });
                }

                return Ok(new { message = "Type deleted." });
            }
            else
            {
                return Unauthorized(new { message = "User unauthorized.", role });
            }

        }

    }
}
