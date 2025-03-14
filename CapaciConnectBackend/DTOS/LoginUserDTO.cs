using System.ComponentModel.DataAnnotations;

namespace CapaciConnectBackend.DTOS
{
    public class LoginUserDTO
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; } = string.Empty;

        [Required]
        public string? Password { get; set; } = string.Empty;
    }
}
