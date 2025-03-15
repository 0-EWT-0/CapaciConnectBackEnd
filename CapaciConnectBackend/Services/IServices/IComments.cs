using CapaciConnectBackend.DTOS;
using CapaciConnectBackend.DTOS.Comments;
using CapaciConnectBackend.Models.Domain;
using System.Runtime.InteropServices;

namespace CapaciConnectBackend.Services.IServices
{
    public interface IComments
    {
        Task<List<Comments>> GetAllCommentsAsync();
        Task<Comments?> GetCommentByWorkshopIdAsync(int workshopId);
        Task<Comments?> UpdateCommentAsync(int commentId, UpdateCommentDTO commentDTO); 
        Task<Comments?> CreateCommentAsync(CommentDTO commentDTO, int userId);
        Task<bool> DeleteAllWorkshopCommentsAsync(int workshopId);
        Task<bool> DeleteCommentAsync (int commentId);
    }
}
