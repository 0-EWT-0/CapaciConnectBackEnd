using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CapaciConnectBackend.Models.Domain
{
    public class WorkshopTypes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_type { get; set; }

        [Required]
        [StringLength(100)]
        public string Type_name { get; set; } = string.Empty;

        public virtual ICollection<Workshops> Workshops { get; set; } = new HashSet<Workshops>();
    }
}
