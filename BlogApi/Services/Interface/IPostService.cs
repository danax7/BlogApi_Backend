using BlogApi.DTO.PostDTO;

namespace BlogApi.Service.Interface;

public interface IPostService
{
    Task<PostPagedListDto> GetPosts(PostFilterDto postFilterDto);

    Task<Guid> CreatePost(CreatePostDto createPostDto, List<Guid> tagIds, Guid userId);

    Task<PostFullDto> GetPostById(Guid id);
    // Task<Boolean> CheckIfUserCanRatePost(Guid idPost, Guid idUser);
    // Task LikePost(Guid idPost, Guid idUser);
    // Task deleteLikePost(Guid idPost, Guid idUser);
}