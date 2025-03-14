using CapaciConnectBackend.DTOS.Users;
using CapaciConnectBackend.Models.Domain;
using CapaciConnectBackend.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPut("UpdateUserAdmin/{userId}")]
        public async Task<IActionResult> UpdateAdminUser([FromRoute] int userId, [FromBody] UpdateUserAdminDTO userDTO)
        {

            var updatedUser = await _userService.UpdateUserAdminAsync(userId, userDTO);

            if (updatedUser == null)
            {
                return NotFound(new { message = "User not found." });
            }

            return Ok(updatedUser);

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

            var deletedUser = await _userService.DeleteUserByIdAsync(userId);

            if (!deletedUser)
            {
                return NotFound(new { message = "User not found." });
            }

            return Ok(deletedUser);

        }
    }

}



