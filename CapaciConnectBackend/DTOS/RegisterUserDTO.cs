using System.ComponentModel.DataAnnotations;

namespace CapaciConnectBackend.DTOS
{
    public class RegisterUserDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Last_names { get; set; } = string.Empty;

        [Required]
        public string Phone { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required, Compare(nameof(Password))]
        public string? Confirmpassword { get; set; } = string.Empty;

    }
}
