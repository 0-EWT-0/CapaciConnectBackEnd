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
        private readonly IError _errorService;
        public CommentService(AplicationDBContext context, IConfiguration configuration, IError errorService)
        {
            _context = context;
            _configuration = configuration;
            _errorService = errorService;
        }

        public async Task<List<Comments>> GetAllCommentsAsync()
        {
            try
            {
                var comments = await _context.Comments.AsNoTracking().ToListAsync();

                return comments;
            }
            catch (Exception ex)
            {
                var log = await _errorService.SaveErrorLogAsync($"Error in Get Comments: {ex.Message}");

                if (log == null)
                {
                    Console.WriteLine("Error log could not be saved.");
                }

                return new List<Comments>();
            }
        }

        public async Task<List<Comments>> GetCommentByWorkshopIdAsync(int workshopId)
        {
            try
            {
                var comments = await _context.Comments.Where(c => c.Id_workshop_id == workshopId).AsNoTracking().ToListAsync();

                return comments;
            }
            catch (Exception ex)
            {
                var log = await _errorService.SaveErrorLogAsync($"Error in Get Comments: {ex.Message}");

                if (log == null)
                {
                    Console.WriteLine("Error log could not be saved.");
                }

                return new List<Comments>(); // deberia regresar null u otra cosa
            }
        }

        public async Task<Comments?> CreateCommentAsync(CommentDTO commentDTO, int userId)
        {
            try
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
            catch (Exception ex)
            {
                var log = await _errorService.SaveErrorLogAsync($"Error in Post Comments: {ex.Message}");

                if (log == null)
                {
                    Console.WriteLine("Error log could not be saved.");
                }

                return null;
            }

        }

        public async Task<Comments?> UpdateCommentAsync(int commentId, UpdateCommentDTO commentDTO)
        {
            try
            {
                var comment = await _context.Comments.FindAsync(commentId);

                if (comment == null) return null;

                comment.Comment = commentDTO.Comment;

                await _context.SaveChangesAsync();

                return comment;

            }
            catch (Exception ex)
            {
                var log = await _errorService.SaveErrorLogAsync($"Error in Put Comments: {ex.Message}");

                if (log == null)
                {
                    Console.WriteLine("Error log could not be saved.");
                }

                return null;
            }
        }

        public async Task<bool> DeleteAllWorkshopCommentsAsync(int workshopId)
        {
            try
            {
                var comments = await _context.Comments.Where(c => c.Id_workshop_id == workshopId).ToListAsync();

                if (!comments.Any()) return false;

                _context.Comments.RemoveRange(comments);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                var log = await _errorService.SaveErrorLogAsync($"Error in Delete Comments: {ex.Message}");

                if (log == null)
                {
                    Console.WriteLine("Error log could not be saved.");
                }

                return false;
            }

        }

        public async Task<bool> DeleteCommentAsync(int commentId)
        {
            try
            {
                var comment = await _context.Comments.FindAsync(commentId);

                if (comment == null) return false;

                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                var log = await _errorService.SaveErrorLogAsync($"Error in Delete Comment: {ex.Message}");

                if (log == null)
                {
                    Console.WriteLine("Error log could not be saved.");
                }

                return false;
            }
        }

    }
}
