using BlogApi.DTO.PostDTO;
using BlogApi.Entity;

namespace BlogApi.Repository.Interface;

public interface IPostRepository
{
    Task<List<PostEntity>> GetPosts(PostFilterDto postFilterDto, int start, int count);
    Task<PostEntity?> GetPostById(Guid id);
    Task<Int32> GetPostCount(PostFilterDto postFilterDto);
    Task <Guid> CreatePost(PostEntity postEntity);
    //TODO: set LikePost

    // Task<List<PostEntity>> GetCommunityPostList(Guid id, PostFilterDto postFilterDto);
}