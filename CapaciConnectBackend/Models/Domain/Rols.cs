using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CapaciConnectBackend.Models.Domain
{
    public class Roles
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_rol { get; set; }

        [Required]
        [StringLength(100)]
        public string Rol_name { get; set; } = string.Empty;

        public string? Description { get; set; } 

        public virtual ICollection<Users> Users { get; set; } = new HashSet<Users>();
    }
}
