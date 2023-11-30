using BlogApi.DTO.CommentDTO;

namespace BlogApi.Services.Interface;

public interface ICommentService
{
    Task<List<CommentDto>> GetAllFirstLevelCommentReplies(Guid id);
    Task CreateComment(Guid id, Guid userId, CreateCommentDto commentCreateDto);
    Task UpdateComment(Guid id, Guid userId, UpdateCommentDto commentUpdateDto);
    Task DeleteComment(Guid id, Guid userId);
}