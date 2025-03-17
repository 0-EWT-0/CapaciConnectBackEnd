using System.ComponentModel.DataAnnotations;

namespace CapaciConnectBackend.DTOS.Reports
{
    public class UpdateReportDTO
    {
        [Required]
        public string Tittle { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;
    }
}
