using System.ComponentModel.DataAnnotations;

namespace CapaciConnectBackend.DTOS
{
    public class WorkshopTypeDTO
    {
        [Required]
        [StringLength(100)]
        public string Type_name { get; set; } = string.Empty;
    }
}
