using BlogApi.DTO.CommentDTO;
using BlogApi.Helpers;
using BlogApi.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("api/")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("comment/{id}/tree")]
        public async Task<List<CommentDto>> GetCommentTree(Guid id)
        {
            return await _commentService.GetAllFirstLevelCommentReplies(id);
        }

        [Authorize(Policy = "ValidateToken")]
        [HttpPost("post/{id}/comment")]
        public async Task<ActionResult> CreateComment(Guid id, CreateCommentDto commentCreateDto)
        {
            var userId = Converter.GetId(HttpContext);
            await _commentService.CreateComment(id, userId, commentCreateDto);
            return Ok("Comment created successfully");
        }

        [Authorize(Policy = "ValidateToken")]
        [HttpPut("comment/{id}")]
        public async Task<ActionResult> EditComment(Guid id, UpdateCommentDto commentUpdateDto)
        {
            var userId = Converter.GetId(HttpContext);
            await _commentService.UpdateComment(id, userId, commentUpdateDto);
            return Ok("Comment edited successfully");
        }

        [Authorize(Policy = "ValidateToken")]
        [HttpDelete("comment/{id}")]
        public async Task<ActionResult> DeleteComment(Guid id)
        {
            var userId = Converter.GetId(HttpContext);
            await _commentService.DeleteComment(id, userId);
            return Ok("Comment deleted successfully");
        }
    }
}