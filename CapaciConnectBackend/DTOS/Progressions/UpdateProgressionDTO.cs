using System.ComponentModel.DataAnnotations;

namespace CapaciConnectBackend.DTOS.Progressions
{
    public class UpdateProgressionDTO
    {
        [Required]
        [StringLength(100)]
        public string Progression_status { get; set; } = string.Empty;
    }
}
