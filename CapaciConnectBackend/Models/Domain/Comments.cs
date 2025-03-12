using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CapaciConnectBackend.Models.Domain
{
    public class Comments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_comment { get; set; }

        [Required]
        public string Comment { get; set; } = string.Empty;

        [Required]
        public DateTime Created_at { get; set; } = DateTime.UtcNow; 

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
