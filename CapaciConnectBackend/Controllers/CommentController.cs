using CapaciConnectBackend.DTOS;
using CapaciConnectBackend.DTOS.Comments;
using CapaciConnectBackend.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CapaciConnectBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentController : ControllerBase
    {
        private readonly IComments _commentsService;

        public CommentController(IComments comments)
        {
            _commentsService = comments;
        }

        [HttpGet("AllComments")]
        public async Task<IActionResult> GetAllComments()
        {
            var role = User.FindFirstValue(ClaimTypes.Role);

            if (role == "1")
            {
                var comments = await _commentsService.GetAllCommentsAsync();
                return Ok(comments);
            }
            else
            {
                return Unauthorized(new { message = "User unauthorized.", role });
            }
        }

        [HttpGet("CommentByWorkshopId/{workshopId}")]
        public async Task<IActionResult> GetCommentByWorkshopId([FromRoute] int workshopId)
        {
            var comments = await _commentsService.GetCommentByWorkshopIdAsync(workshopId);

            if (comments == null)
            {
                return NotFound(new { message = "Workshop not found." });
            }

            return Ok(comments);
        }

        [HttpPost("CreateComment")]

        public async Task<IActionResult> CreateComment([FromBody] CommentDTO commentDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized(new { message = "User unauthorized." });
            }

            var createdComment = await _commentsService.CreateCommentAsync(commentDTO, int.Parse(userId));

            if(createdComment == null)
            {
                return NotFound(new { message = "Workshop Not Found"});
            }
            return Ok(createdComment);
        }

        [HttpPut("UpdateComment/{commentId}")]
        public async Task<IActionResult> UpdateComment([FromRoute] int commentId, [FromBody] UpdateCommentDTO commentDTO)
        {
            var updatedComment = await _commentsService.UpdateCommentAsync(commentId, commentDTO);

            if (updatedComment == null)
            {
                return NotFound(new { message = "Comment not found." });
            }

            return Ok(updatedComment);
        }

        [HttpDelete("DeleteComment/{commentId}")]

        public async Task<IActionResult> DeleteComment([FromRoute] int commentId)
        {
            var deletedComment = await _commentsService.DeleteCommentAsync(commentId);

            if (!deletedComment)
            {
                return NotFound(new { message = "Comment not found." });
            }

            return Ok(deletedComment);
        }

        [HttpDelete("DeleteAllWorkshopComments/{workshopId}")]

        public async Task<IActionResult> DeleteAllComments([FromRoute] int workshopId)
        {
            var role = User.FindFirstValue(ClaimTypes.Role);

            if (role == "1")
            {
                var comment = await _commentsService.DeleteAllWorkshopCommentsAsync(workshopId);

                if (!comment)
                {
                    return NotFound(new { message = "Workshop not found." });
                }

                return Ok(comment);
            }
            else
            {
                return Unauthorized(new { message = "User unauthorized.", role });
            }
        }
    }
}
