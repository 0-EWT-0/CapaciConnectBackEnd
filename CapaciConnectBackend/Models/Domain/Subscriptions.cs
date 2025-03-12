using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CapaciConnectBackend.Models.Domain
{
    public class Subscriptions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_subscription { get; set; }

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
