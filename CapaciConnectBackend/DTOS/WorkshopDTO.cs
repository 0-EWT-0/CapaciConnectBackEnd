using System.ComponentModel.DataAnnotations;

namespace CapaciConnectBackend.DTOS
{
    public class WorkshopDTO
    {
        [Required]
        [StringLength(255)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        public string? Image { get; set; } = string.Empty;

        [Required]
        public int Id_type_id { get; set; }

    }
}
