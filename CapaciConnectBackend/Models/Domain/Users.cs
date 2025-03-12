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
        public virtual Rols? Rol { get; set; }

        public virtual ICollection<Workshops> Workshops { get; set; } = new HashSet<Workshops>();
        public virtual ICollection<Comments> Comments { get; set; } = new HashSet<Comments>();
        public virtual ICollection<Logs> Logs { get; set; } = new HashSet<Logs>();
        public virtual ICollection<Progressions> Progressions { get; set; } = new HashSet<Progressions>();
        public virtual ICollection<Sessions> Sessions { get; set; } = new HashSet<Sessions>();
        public virtual ICollection<Subscriptions> Subscriptions { get; set; } = new HashSet<Subscriptions>();
        public virtual ICollection<Reports> Reports { get; set; } = new HashSet<Reports>();
    }
}
