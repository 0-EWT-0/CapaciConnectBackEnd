using System.ComponentModel.DataAnnotations;

namespace CapaciConnectBackend.DTOS
{
    public class CommentDTO
    {

        [Required]
        public string Comment { get; set; } = string.Empty;

        [Required]
        public int Id_workshop_id { get; set; }
    }
}
