using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CapaciConnectBackend.Models.Domain
{
    public class Progressions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_progression { get; set; }

        [Required]
        [StringLength(100)]
        public string Progression_status { get; set; } = string.Empty;

        [Required]
        public int Id_user_id { get; set; }

        [ForeignKey("Id_user_id")]
        public virtual Users? User { get; set; }

        [Required]
        public int Id_workshop_id { get; set; }

        [ForeignKey("Id_workshop_id")]
        public virtual Workshops? Workshop { get; set; }
    }
}
