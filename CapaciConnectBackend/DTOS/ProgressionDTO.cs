using System.ComponentModel.DataAnnotations;

namespace CapaciConnectBackend.DTOS
{
    public class ProgressionDTO
    {
        [Required]
        [StringLength(100)]
        public string Progression_status { get; set; } = string.Empty;

        [Required]
        public int Id_workshop_id { get; set; }
    }
}
