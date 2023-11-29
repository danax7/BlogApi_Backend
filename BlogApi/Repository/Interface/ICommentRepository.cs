using BlogApi.DTO.CommentDTO;
using BlogApi.Entity;

namespace BlogApi.Repository.Interface;

public interface ICommentRepository
{
    Task<List<CommentDto>> GetCommentTree(Guid id);
    Task<List<CommentDto>> GetAllFirstLevelPostCommentsById(Guid postId);
    Task CreateComment(CommentEntity comment);
    Task UpdateComment(CommentEntity comment);
    Task DeleteComment(Guid id);
    Task<CommentEntity> GetCommentById(Guid id);
}