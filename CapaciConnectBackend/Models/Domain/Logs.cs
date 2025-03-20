using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CapaciConnectBackend.Models.Domain
{
    public class Logs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_log { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty; 

        [Required]
        public DateTime Created_at { get; set; } = DateTime.UtcNow; 

        //public int Id_user_id { get; set; }

        //[ForeignKey("Id_user_id")]
        //public virtual Users? User { get; set; }
    }
}
