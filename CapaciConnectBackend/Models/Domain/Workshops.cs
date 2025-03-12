using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CapaciConnectBackend.Models.Domain
{
    public class Workshops
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_workshop { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        [Required]
        public DateTime Created_at { get; set; } = DateTime.UtcNow;

        [Required]
        public int Id_user_id { get; set; }

        [ForeignKey("Id_user_id")]
        public virtual Users? User { get; set; }

        [Required]
        public int Id_type_id { get; set; }

        [ForeignKey("Id_type_id")]
        public virtual WorkshopTypes? WorkshopType { get; set; }

        public virtual ICollection<Calendars> Calendars { get; set; } = new HashSet<Calendars>();
        public virtual ICollection<Comments> Comments { get; set; } = new HashSet<Comments>();
        public virtual ICollection<Progressions> Progressions { get; set; } = new HashSet<Progressions>();
        public virtual ICollection<Subscriptions> Subscriptions { get; set; } = new HashSet<Subscriptions>();
        public virtual ICollection<WorkshopMultimedia> WorkshopMultimedia { get; set; } = new HashSet<WorkshopMultimedia>();
        public virtual ICollection<Reports> Reports { get; set; } = new HashSet<Reports>();
    }
}
