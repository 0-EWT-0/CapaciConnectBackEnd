using CapaciConnectBackend.DTOS.Responses;

namespace CapaciConnectBackend.Server.DTOS.Responses
{
    public record RegistrationResponse(bool Flag, string Message = null!, UserAuthResponse? User = null);

}
