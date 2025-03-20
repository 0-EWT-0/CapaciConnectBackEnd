
using CapaciConnectBackend.DTOS.Responses;

namespace CapaciConnectBackend.DTOS.Responses
{
    public record ErrorResponse(bool Flag, string Message = null!);
}
