using BlogApi.DTO;
using BlogApi.DTO.PostDTO;
using BlogApi.Exception;
using BlogApi.Service.Interface;

namespace BlogApi.Service;

public class PostServiceImpl : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;


    public PostServiceImpl(IPostRepository postRepository, IUserRepository userRepository)
    {
        _postRepository = postRepository;
        _userRepository = userRepository;
    }

    public async Task<PostPagedListDto> GetPosts(PostFilterDto postFilterDto)
    {
       return null;
    }

    public async Task<PostDto> CreatePost(CreatePostDto postCreateDto)
    {
      
        return null;
 
    }

    public async Task<PostDto> GetPostById(Guid id)
    {
        var post = await _postRepository.GetPostById(id);
        if (post == null)
        {
            throw new NotFoundException("Post not found");
        }
        return new PostDto(post);
    }

    public async Task<Boolean> CheckIfUserCanRatePost(Guid idPost, Guid idUser)
    {
        var post = await _postRepository.GetPostById(idPost);
        if (post == null)
        {
            throw new NotFoundException("Post not found");
        }

        var user = await _userRepository.GetUserById(idUser);
        if (user == null)
        {
            throw new NotFoundException("User not found");
        }

        return await _postRepository.CheckIfUserCanRatePost(idPost, idUser);
    }

    public async Task LikePost(Guid idPost, Guid idUser)
    {
        var post = await _postRepository.GetPostById(idPost);
        if (post == null)
        {
            throw new NotFoundException("Post not found");
        }

        var user = await _userRepository.GetUserById(idUser);
        if (user == null)
        {
            throw new NotFoundException("User not found");
        }

        await _postRepository.LikePost(idPost, idUser);
    }

    public async Task deleteLikePost(Guid idPost, Guid idUser)
    {
        var post = await _postRepository.GetPostById(idPost);
        if (post == null)
        {
            throw new NotFoundException("Post not found");
        }

        var user = await _userRepository.GetUserById(idUser);
        if (user == null)
        {
            throw new NotFoundException("User not found");
        }

        await _postRepository.deleteLikePost(idPost, idUser);
    }
}