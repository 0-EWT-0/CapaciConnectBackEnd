using System.ComponentModel.DataAnnotations;

namespace CapaciConnectBackend.DTOS.Comments
{
    public class UpdateCommentDTO
    {

        [Required]
        public string Comment { get; set; } = string.Empty;
    }
}
