using System.ComponentModel.DataAnnotations;

namespace CapaciConnectBackend.DTOS
{
    public class CalendarDTO
    {
        [Required]
        public DateTime Date_start { get; set; }

        [Required]
        public DateTime Date_end { get; set; }

        [Required]
        public int Id_workshop_id { get; set; }
    }
}
