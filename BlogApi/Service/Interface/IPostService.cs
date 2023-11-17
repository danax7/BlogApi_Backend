using BlogApi.DTO.PostDTO;

namespace BlogApi.Service.Interface;

public interface IPostService
{
    Task<PostPagedListDto> GetPosts(PostFilterDto postFilterDto);
    Task<PostDto> CreatePost(CreatePostDto postCreateDto);
    Task<PostDto> GetPostById(Guid id);
    Task<Boolean> CheckIfUserCanRatePost(Guid idPost, Guid idUser);
    Task LikePost(Guid idPost, Guid idUser);
    Task deleteLikePost(Guid idPost, Guid idUser);
}