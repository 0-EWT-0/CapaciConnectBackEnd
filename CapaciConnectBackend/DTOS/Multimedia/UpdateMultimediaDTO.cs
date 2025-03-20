using System.ComponentModel.DataAnnotations;

namespace CapaciConnectBackend.DTOS.Multimedia
{
    public class UpdateMultimediaDTO
    {
        [Required]
        [StringLength(255)]
        public string Media_url { get; set; } = string.Empty;
    }
}
