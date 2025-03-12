using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CapaciConnectBackend.Models.Domain
{
    public class WorkshopMultimedia
    {
        [Required]
        public int Id_workshop_id { get; set; }

        [ForeignKey("Id_workshop_id")]
        public virtual Workshops? Workshop { get; set; }

        [Required]
        public int Id_multimedia_id { get; set; }

        [ForeignKey("Id_multimedia_id")]
        public virtual Multimedia? Multimedia { get; set; }
    }
}
