using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CapaciConnectBackend.Models.Domain
{
    public enum MediaType
    {
        Image,
        Video
    }
    public class Multimedia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_multimedia { get; set; }

        [Required]
        public string Media_url { get; set; } = string.Empty; 

        [Required]
        [StringLength(10)]
        [Column(TypeName = "VARCHAR(10)")]
        public MediaType Media_type { get; set; }

        public virtual ICollection<WorkshopMultimedia> WorkshopMultimedia { get; set; } = new HashSet<WorkshopMultimedia>();
    }
}
