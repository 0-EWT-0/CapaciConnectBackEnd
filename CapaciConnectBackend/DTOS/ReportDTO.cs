using System.ComponentModel.DataAnnotations;

namespace CapaciConnectBackend.DTOS
{
    public class ReportDTO
    {
        [Required]
        public string Tittle { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        [Required]
        public int Id_workshop_id { get; set; }
    }
}
