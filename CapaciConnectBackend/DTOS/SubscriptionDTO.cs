using System.ComponentModel.DataAnnotations;

namespace CapaciConnectBackend.DTOS
{
    public class SubscriptionDTO
    {
        [Required]
        public int Id_workshop_id { get; set; }
    }
}
