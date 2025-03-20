using CapaciConnectBackend.DTOS.Users;
using CapaciConnectBackend.Models.Domain;
using CapaciConnectBackend.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CapaciConnectBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUser _userService;

        public UserController(IUser user)
        {
            _userService = user;
        }

        [HttpGet("AllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("UserById/{userId}")]
        public async Task<IActionResult> GetUserById([FromRoute] int userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            return Ok(user);
        }

        [HttpGet("UserInfo")]

        public async Task<IActionResult> GetUserInfo()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return NotFound(new { message = "User not found." });
            }
            else
            {
                var user = await _userService.GetUserInfo(int.Parse(userId));

                return Ok(user);
            }
        }

        [HttpPut("UpdateUserAdmin/{userId}")]
        public async Task<IActionResult> UpdateAdminUser([FromRoute] int userId, [FromBody] UpdateUserAdminDTO userDTO)
        {
            var role = User.FindFirstValue(ClaimTypes.Role);

            if (role == "1")
            {
                var updatedUser = await _userService.UpdateUserAdminAsync(userId, userDTO);
                if (updatedUser == null)
                {
                    return NotFound(new { message = "User not found." });
                }
                return Ok(updatedUser);
            }
            else
            {
                return Unauthorized(new { message = "User unauthorized.", role });
            }

        }

        [HttpPatch("UpdateUser/{userId}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int userId, [FromBody] UpdateUserDTO userDTO)
        {

            var updatedUser = await _userService.UpdateUserAsync(userId, userDTO);

            if (updatedUser == null)
            {
                return NotFound(new { message = "User not found." });
            }
            return Ok(updatedUser);

        }

        [HttpDelete("DeleteUser/{userId}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int userId)
        {
            var role = User.FindFirstValue(ClaimTypes.Role);

            if (role == "1")
            {
                var deletedUser = await _userService.DeleteUserByIdAsync(userId);

                if (!deletedUser)
                {
                    return NotFound(new { message = "User not found." });
                }

                return Ok(deletedUser);
            }
            else
            {
                return Unauthorized(new { message = "User unauthorized.", role });
            }

        }
    }

}



