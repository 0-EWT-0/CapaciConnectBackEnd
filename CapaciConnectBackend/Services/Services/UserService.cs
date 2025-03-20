using CapaciConnectBackend.Context;
using CapaciConnectBackend.DTOS.Users;
using CapaciConnectBackend.Models.Domain;
using CapaciConnectBackend.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace CapaciConnectBackend.Services.Services
{
    public class UserService : IUser
    {
        private readonly AplicationDBContext _context;
        private readonly IConfiguration _configuration;
        public UserService(AplicationDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        //Get All Users
        public async Task<List<Users>> GetAllUsersAsync()
        {
            var users = await _context.Users.AsNoTracking().ToListAsync();

            return users;
        }

        // Get by ID
        public async Task<Users?> GetUserByIdAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);

            return user;
        }

        // Update only for admins
        public async Task<Users?> UpdateUserAdminAsync(int userId, UpdateUserAdminDTO userDTO)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null) return null;

            user.Name = userDTO.Name;
            user.Last_names = userDTO.Last_names;
            user.Email = userDTO.Email;
            user.Phone = userDTO.Phone;
            user.Profile_img = userDTO.Profile_img;
            user.Description = userDTO.Description;
            user.Id_rol_id = userDTO.Id_rol_id;

            await _context.SaveChangesAsync();

            return user;
        }

        // Update for underprivileged users
        public async Task<Users?> UpdateUserAsync(int userId, UpdateUserDTO userDTO)
        {

            var user = await _context.Users.FindAsync(userId);
            if (user == null) return null;

            user.Name = userDTO.Name;
            user.Last_names = userDTO.Last_names;
            user.Email = userDTO.Email;
            user.Phone = userDTO.Phone;
            user.Profile_img = userDTO.Profile_img;
            user.Description = userDTO.Description;


            await _context.SaveChangesAsync();

            return user;

        }

        // Delete By ID
        public async Task<bool> DeleteUserByIdAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Users>> GetUserInfo(int userId)
        {
            var user = await _context.Users.Where(u => u.Id_user == userId).AsNoTracking().ToListAsync();

            return user;
        }
    }
}
