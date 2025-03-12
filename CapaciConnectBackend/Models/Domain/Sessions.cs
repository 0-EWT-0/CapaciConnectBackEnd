using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CapaciConnectBackend.Models.Domain
{
    public class Sessions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_session { get; set; }

        [Required]
        [StringLength(255)]
        public string Token { get; set; } = string.Empty; 

        [Required]
        public DateTime Created_at { get; set; } = DateTime.UtcNow; 

        [Required]
        public int Id_user_id { get; set; }

        [ForeignKey("Id_user_id")]
        public virtual Users? User { get; set; }
    }
}
