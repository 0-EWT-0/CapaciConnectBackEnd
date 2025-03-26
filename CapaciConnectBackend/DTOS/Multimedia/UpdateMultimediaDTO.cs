using System.ComponentModel.DataAnnotations;

namespace CapaciConnectBackend.DTOS.Multimedia
{
    public class UpdateMultimediaDTO
    {
        [Required]
        public string Media_url { get; set; } = string.Empty;
    }
}
