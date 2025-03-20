using CapaciConnectBackend.DTOS.Users;
using CapaciConnectBackend.Models.Domain;

namespace CapaciConnectBackend.Services.IServices
{
    public interface IUser
    {
       Task<List<Users>> GetUserInfo(int userId);
       Task<List<Users>> GetAllUsersAsync();
       Task<Users?> GetUserByIdAsync(int userId);
       Task<Users?> UpdateUserAdminAsync(int userId, UpdateUserAdminDTO userDTO);
       Task<Users?> UpdateUserAsync(int userId, UpdateUserDTO userDTO);
       Task<bool> DeleteUserByIdAsync(int userId);
    }
}
