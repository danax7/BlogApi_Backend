using BlogApi.DTO.CommentDTO;
using BlogApi.Entity;
using BlogApi.Exception;
using BlogApi.Repository.Interface;
using BlogApi.Services.Interface;

namespace BlogApi.Services;

public class CommentServiceImpl : ICommentService
{
    private readonly IPostRepository _postRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly IUserRepository _userRepository;

    public CommentServiceImpl(IPostRepository postRepository, ICommentRepository commentRepository,
        IUserRepository userRepository)
    {
        _postRepository = postRepository;
        _commentRepository = commentRepository;
        _userRepository = userRepository;
    }

    public async Task<List<CommentDto>> GetCommentTree(Guid id)
    {
        var comments = await _commentRepository.GetCommentTree(id);
        return comments;
    }

    public async Task CreateComment(Guid id, Guid userId, CreateCommentDto commentCreateDto)
    {
        var post = await _postRepository.GetPostById(id);
        var user = await _userRepository.GetUserById(userId);
        if (post == null)
        {
            throw new NotFoundException("Post not found");
        }

        var comment = new CommentEntity
        {
            id = Guid.NewGuid(),
            content = commentCreateDto.content,
            PostId = id,
            UserId = userId,
            authorId = user.Id,
            author = user.FullName,
            ParentCommentId = commentCreateDto.parentId,
            createTime = DateTime.Now
        };

        if (comment.ParentCommentId != null)
        {
            var parentComment = await _commentRepository.GetCommentById(comment.ParentCommentId.Value);
            if (parentComment == null)
            {
                throw new NotFoundException("Parent comment not found");
            }

            parentComment.IncrementSubCommentCount();
        }

        post.IncrementCommentCount();
        await _postRepository.UpdatePost(post);

        await _commentRepository.CreateComment(comment);
    }

    public async Task UpdateComment(Guid id, Guid userId, UpdateCommentDto commentUpdateDto)
    {
        var comment = await _commentRepository.GetCommentById(id);
        if (comment == null)
        {
            throw new NotFoundException("Comment not found");
        }

        if (comment.UserId != userId)
        {
            throw new ForbiddenException("You are not the author of this comment");
        }

        comment.content = commentUpdateDto.content;
        comment.modifiedDate = DateTime.Now;
        await _commentRepository.UpdateComment(comment);
    }

    public async Task DeleteComment(Guid id, Guid userId)
    {
        var comment = await _commentRepository.GetCommentById(id);
        var post = await _postRepository.GetPostById(comment.PostId);

        if (post == null)
        {
            throw new NotFoundException("Post not found");
        }

        if (comment == null)
        {
            throw new NotFoundException("Comment not found");
        }

        if (comment.UserId != userId)
        {
            throw new ForbiddenException("You are not the author of this comment");
        }

        if (comment.ParentCommentId != null)
        {
            var parentComment = await _commentRepository.GetCommentById(comment.ParentCommentId.Value);
            if (parentComment == null)
            {
                throw new NotFoundException("Parent comment not found");
            }
            // parentComment.DecrementSubCommentCount();
        }

        comment.deleteDate = DateTime.Now;

        post.DecrementCommentCount();
        await _postRepository.UpdatePost(post);

        await _commentRepository.DeleteComment(comment.id);
    }
}