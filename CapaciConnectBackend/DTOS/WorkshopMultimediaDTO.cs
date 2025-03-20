using System.ComponentModel.DataAnnotations;

namespace CapaciConnectBackend.DTOS
{
    public class WorkshopMultimediaDTO
    {
        [Required]
        public int Id_workshop_id { get; set; }

        [Required]
        public int Id_multimedia_id { get; set; }

    }
}
