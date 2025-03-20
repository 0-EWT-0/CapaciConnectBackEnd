using CapaciConnectBackend.Context;
using CapaciConnectBackend.DTOS;
using CapaciConnectBackend.Server.DTOS.Responses;
using CapaciConnectBackend.Services.IServices;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.CodeDom.Compiler;
using CapaciConnectBackend.Models.Domain;
using System.Security.Claims;
using System.Text;
using CapaciConnectBackend.DTOS.Responses;

namespace CapaciConnectBackend.Services.Services
{
    public class AuthService : IAuth
    {
        private readonly AplicationDBContext _context;
        private readonly IConfiguration _configuration;
        private readonly IError _errorService;
        public AuthService(AplicationDBContext context, IConfiguration configuration, IError errorS)
        {
            _context = context;
            _configuration = configuration;
            _errorService = errorS;
        }

        private string GenerateJWTToken(Users user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id_user.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Id_rol_id.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(5),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Login
        public async Task<LoginResponse> LoginUserAsync(LoginUserDTO loginUserDTO)
        {
            try
            {
                var getUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginUserDTO.Email);

                if (getUser == null)
                {
                    return new LoginResponse(false, "User Not Found");
                }

                bool checkPassword = BCrypt.Net.BCrypt.Verify(loginUserDTO.Password, getUser.Password);

                if (!checkPassword)
                {
                    return new LoginResponse(false, "Invalid Credentials");
                }

                var oldSessions = _context.Sessions.Where(s => s.Id_user_id == getUser.Id_user);
                _context.Sessions.RemoveRange(oldSessions);
                await _context.SaveChangesAsync();

                string token = GenerateJWTToken(getUser);

                var newSession = new Sessions
                {
                    Token = token,
                    Created_at = DateTime.Now,
                    Id_user_id = getUser.Id_user
                };

                _context.Sessions.Add(newSession);
                await _context.SaveChangesAsync();

                var userResponse = new UserAuthResponse
                {
                    Id_user = getUser.Id_user,
                    Name = getUser.Name,
                    Last_names = getUser.Last_names,
                    Phone = getUser.Phone,
                    Email = getUser.Email,
                    Id_rol_id = getUser.Id_rol_id
                };
                return new LoginResponse(true, "Login Success", token, userResponse);
            }
            catch (Exception ex)
            {
                var log = await _errorService.SaveErrorLogAsync($"Error in Login: {ex.Message}");

                if (log == null)
                {
                    Console.WriteLine("Error log could not be saved.");
                }

                return new LoginResponse(false, "An error has ocurred in login");
            }
        }

        //Register
        public async Task<RegistrationResponse> RegisterUserAsync(RegisterUserDTO registerUserDTO)
        {
            try
            {
                var exists = await _context.Users.AnyAsync(u => u.Email == registerUserDTO.Email);

                if (exists)
                {
                    var log = new RegistrationResponse(false, "User Already Exists");
                    var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == registerUserDTO.Email);

                    var newLog = new Logs
                    {
                        Content = log.ToString(),
                        Created_at = DateTime.Now,
                        //Id_user_id = existingUser.Id_user
                    };
                    _context.Logs.Add(newLog);
                    await _context.SaveChangesAsync();
                    return log;
                }
                else
                {
                    var newUser = new Users()
                    {
                        Name = registerUserDTO.Name,
                        Last_names = registerUserDTO.Last_names,
                        Phone = registerUserDTO.Phone,
                        Email = registerUserDTO.Email,
                        Password = BCrypt.Net.BCrypt.HashPassword(registerUserDTO.Password),
                        Created_at = DateTime.Now,
                        Id_rol_id = 4 // Participante
                    };

                    _context.Users.Add(newUser);
                    await _context.SaveChangesAsync();

                    return new RegistrationResponse(true, "User Created Successfully");

                }
            }
            catch (Exception ex)
            {
                var log = await _errorService.SaveErrorLogAsync($"Error in Register: {ex.Message}");

                if (log == null)
                {
                    Console.WriteLine("Error log could not be saved.");
                }

                return new RegistrationResponse(false, "An error has ocurred in Register");
            }

        }

        public async Task<LogoutResponse> LogoutUserAsync(int userId)
        {
            try
            {
                var existingSessions = await _context.Sessions
                    .Where(s => s.Id_user_id == userId)
                    .ToListAsync();

                if (!existingSessions.Any())
                {
                    return new LogoutResponse(false, "User is already logged out or session does not exist.");
                }

                _context.Sessions.RemoveRange(existingSessions);
                await _context.SaveChangesAsync();

                return new LogoutResponse(true, "Logout successful.");
            }
            catch (Exception ex)
            {
                var log = await _errorService.SaveErrorLogAsync($"Error in Logout: {ex.Message}");

                if (log == null)
                {
                    Console.WriteLine("Error log could not be saved.");
                }

                return new LogoutResponse(false, "An error has ocurred in logout.");
            }
        }

    }
}
