using CapaciConnectBackend.DTOS;
using CapaciConnectBackend.Server.DTOS.Responses;
using CapaciConnectBackend.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace CapaciConnectBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuth _authService;

        public AuthController(IAuth authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> LoginUser([FromBody] LoginUserDTO loginUserDTO)
        {
            try
            {
                if (loginUserDTO == null)
                {
                    return BadRequest(new LoginResponse(false, "Invalid request body"));
                }

                var result = await _authService.LoginUserAsync(loginUserDTO);

                if (!result.Flag)
                {
                    if (result.Message.Equals("Invalid Credentials", StringComparison.OrdinalIgnoreCase) ||
                        result.Message.Equals("User Not Found", StringComparison.OrdinalIgnoreCase))
                    {
                        return Unauthorized(result);
                    }

                    return BadRequest(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException != null ? $"{ex.Message} - {ex.InnerException.Message}" : ex.Message;
                return StatusCode(500, new LoginResponse(false, errorMessage));
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> RegisterUser([FromBody] RegisterUserDTO registerUserDTO)
        {
            try
            {
                if (registerUserDTO == null)
                {
                    return BadRequest(new RegistrationResponse(false, "Invalid request body"));
                }

                var result = await _authService.RegisterUserAsync(registerUserDTO);

                if (!result.Flag)
                {
                    if (result.Message.Equals("User Already Exists", StringComparison.OrdinalIgnoreCase))
                    {
                        return BadRequest(result);
                    }

                    return BadRequest(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException != null ? $"{ex.Message} - {ex.InnerException.Message}" : ex.Message;
                return StatusCode(500, new RegistrationResponse(false, errorMessage));
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogoutUser()
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

                if (userId == 0)
                {
                    return Unauthorized(new { message = "Invalid user.", userId });
                }

                var result = await _authService.LogoutUserAsync(userId);

                if (!result.Success)
                {
                    return BadRequest(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

    }
}
