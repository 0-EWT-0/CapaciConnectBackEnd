using System.ComponentModel.DataAnnotations;

namespace CapaciConnectBackend.DTOS.Calendars
{
    public class UpdateCalendarDTO
    {
        [Required]
        public DateTime Date_start { get; set; }

        [Required]
        public DateTime Date_end { get; set; }
    }
}
