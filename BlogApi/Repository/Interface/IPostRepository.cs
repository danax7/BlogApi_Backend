using BlogApi.DTO.PostDTO;
using BlogApi.Entity;

namespace BlogApi.Repository.Interface;

public interface IPostRepository
{
    Task<List<PostEntity>> GetPosts(PostFilterDto postFilterDto, int start, int count);
    Task<PostEntity?> GetPostById(Guid id);
    Task<Int32> GetPostCount(PostFilterDto postFilterDto);
    Task<Guid> CreatePost(PostEntity postEntity);
    Task LikePost(Guid postId, Guid userId);
    Task DeletePostLike(Guid postId, Guid userId);
    Task UpdatePost(PostEntity postEntity);

    // Task<List<PostEntity>> GetCommunityPostList(Guid id, PostFilterDto postFilterDto);
}