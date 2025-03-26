using System.ComponentModel.DataAnnotations;

namespace CapaciConnectBackend.DTOS
{
    public class MultimediaDTO
    {
        [Required]
        public string Media_url { get; set; } = string.Empty;

        [Required]
        public string Media_type { get; set; } = string.Empty;
    }
}
