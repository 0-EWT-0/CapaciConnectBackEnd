using CapaciConnectBackend.Context;
using CapaciConnectBackend.DTOS;
using CapaciConnectBackend.DTOS.Comments;
using CapaciConnectBackend.Models.Domain;
using CapaciConnectBackend.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace CapaciConnectBackend.Services.Services
{
    public class CommentService : IComments
    {
        private readonly AplicationDBContext _context;
        private readonly IConfiguration _configuration;
        public CommentService(AplicationDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<Comments>> GetAllCommentsAsync()
        {
            var comments = await _context.Comments.AsNoTracking().ToListAsync();

            return comments;
        }

        public async Task<List<Comments>> GetCommentByWorkshopIdAsync(int workshopId)
        {
            var comments = await _context.Comments.Where(c => c.Id_workshop_id == workshopId).AsNoTracking().ToListAsync();

            return comments;
        }

        public async Task<Comments?> CreateCommentAsync(CommentDTO commentDTO, int userId)
        {
            var newComment = new Comments
            {
                Comment = commentDTO.Comment,
                Created_at = DateTime.Now,
                Id_user_id = userId,
                Id_workshop_id = commentDTO.Id_workshop_id,
            };

            _context.Comments.Add(newComment);
            await _context.SaveChangesAsync();

            return newComment;
        }

        public async Task<Comments?> UpdateCommentAsync(int commentId, UpdateCommentDTO commentDTO)
        {
            var comment = await _context.Comments.FindAsync(commentId);

            if (comment == null) return null;

            comment.Comment = commentDTO.Comment;

            await _context.SaveChangesAsync();

            return comment;
        }

        public async Task<bool> DeleteAllWorkshopCommentsAsync(int workshopId)
        {
            var comments = await _context.Comments.Where(c => c.Id_workshop_id == workshopId).ToListAsync();

            if (!comments.Any()) return false;

            _context.Comments.RemoveRange(comments);
            await _context.SaveChangesAsync();

            return true;

        }

        public async Task<bool> DeleteCommentAsync(int commentId)
        {
            var comment = await _context.Comments.FindAsync(commentId);

            if (comment == null) return false;

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
