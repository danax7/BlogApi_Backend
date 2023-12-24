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
    private readonly ICommunityService _communityService;

    public CommentServiceImpl(
        IPostRepository postRepository, 
        ICommentRepository commentRepository,
        IUserRepository userRepository,
        ICommunityService communityService)
    {
        _postRepository = postRepository;
        _commentRepository = commentRepository;
        _userRepository = userRepository;
        _communityService = communityService;
    }

    public async Task<List<CommentDto>> GetAllFirstLevelCommentReplies(Guid id)
    {
        var comments = await _commentRepository.GetAllFirstLevelCommentReplies(id);
        return comments;
    }

    public async Task CreateComment(Guid id, Guid userId, CreateCommentDto commentCreateDto)
    {
        var post = await _postRepository.GetPostById(id);
        if (post == null)
        {
            throw new NotFoundException("Post not found");
        }

        var user = await _userRepository.GetUserById(userId);
        if (user == null)
        {
            throw new NotFoundException("User not found");
        }

        if (post.communityId != null && post.Community.isClosed)
        {
            var userRoleInCommunity = await _communityService.GetGreatestUserCommunityRole(userId, post.communityId.Value);
            if (userRoleInCommunity == null)
            {
                throw new ForbiddenException("You are not a member of this community");
            }
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

        if (comment == null)
        {
            throw new NotFoundException("Comment not found");
        }

        var post = await _postRepository.GetPostById(comment.PostId);
        if (post == null)
        {
            throw new NotFoundException("Post not found");
        }

        if (comment.UserId != userId)
        {
            throw new ForbiddenException("You are not the author of this comment");
        }
        
        if (comment.ParentCommentId != null && comment.subCommentsCount == 0)
        {
            var parentComment = await _commentRepository.GetCommentById(comment.ParentCommentId.Value);
            if (parentComment == null)
            {
                throw new NotFoundException("Parent comment not found");
            }

            parentComment.DecrementSubCommentCount();
            await _commentRepository.DeleteComment(comment.id);
            
            //Удаление удаленного родителя, если у него нет дочерних комментариев
            if (parentComment.subCommentsCount == 0 && parentComment.content == "[deleted]")
            {
                await _commentRepository.DeleteComment(parentComment.id);
            }
            else
            {
                parentComment.deleteDate = DateTime.Now;
                parentComment.modifiedDate = DateTime.Now;
                parentComment.content = "[deleted]";
                parentComment.author = "[deleted]";
                parentComment.authorId = Guid.Empty;
                await _commentRepository.UpdateComment(parentComment);
            }
            ////////////////////////////////
        }
        else if (comment.ParentCommentId == null && comment.subCommentsCount == 0)
        {
            await _commentRepository.DeleteComment(comment.id);
        }
        else
        {
            comment.deleteDate = DateTime.Now;
            comment.modifiedDate = DateTime.Now;
            comment.content = "[deleted]";
            comment.author = "[deleted]";
            comment.authorId = Guid.Empty;
            await _commentRepository.UpdateComment(comment);
        }

        post.DecrementCommentCount();
        await _postRepository.UpdatePost(post);
    }
}