using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace CapaciConnectBackend.Models.Domain
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_user { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string Last_names { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Phone { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; } = string.Empty;

        [StringLength(255)]
        public string? Profile_img { get; set; } 

        public string? Description { get; set; } 

        [Required]
        public DateTime Created_at { get; set; } = DateTime.UtcNow; 

        [Required]
        public int Id_rol_id { get; set; } 

        [ForeignKey("Id_rol_id")]
        public virtual Roles? Rol { get; set; }
    }
}
