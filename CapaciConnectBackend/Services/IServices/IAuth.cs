using CapaciConnectBackend.DTOS;
using CapaciConnectBackend.DTOS.Responses;
using CapaciConnectBackend.Server.DTOS.Responses;

namespace CapaciConnectBackend.Services.IServices
{
    public interface IAuth
    {
        Task<RegistrationResponse> RegisterUserAsync(RegisterUserDTO registerUserDTO);
        Task<LoginResponse> LoginUserAsync(LoginUserDTO loginUserDTO);
        Task<LogoutResponse> LogoutUserAsync(int UserId);
    }
}
