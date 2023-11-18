using BlogApi.DTO.PostDTO;
using BlogApi.Entity;

namespace BlogApi.Repository.Interface;

public interface IPostRepository
{
    Task<List<PostEntity>> GetPosts(PostFilterDto postFilterDto, int start, int count);
    Task<PostEntity?> GetPostById(Guid id);
    Task<Int32> GetPostCount(PostFilterDto postFilterDto);
    //TODO: set LikePost
    
}