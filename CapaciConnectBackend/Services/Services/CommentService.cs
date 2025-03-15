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

        public async Task<Comments?> GetCommentByWorkshopIdAsync(int workshopId)
        {
            var comments = await _context.Comments.FindAsync(workshopId);

            return comments;
        }

        public Task<Comments?> CreateCommentAsync(CommentDTO commentDTO, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<Comments?> UpdateCommentAsync(int commentId, UpdateCommentDTO commentDTO)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAllWorkshopCommentsAsync(int workshopId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCommentAsync(int commentId)
        {
            throw new NotImplementedException();
        }

    }
}
