using CapaciConnectBackend.DTOS.Responses;

namespace CapaciConnectBackend.Server.DTOS.Responses
{
    public record LoginResponse(bool Flag, string Message = null!, string Token = null!, UserAuthResponse? User = null);

}
