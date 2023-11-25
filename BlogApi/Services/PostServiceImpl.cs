using BlogApi.DTO;
using BlogApi.DTO.PostDTO;
using BlogApi.Entity;
using BlogApi.Exception;
using BlogApi.Repository.Interface;
using BlogApi.Service.Interface;

namespace BlogApi.Service;

public class PostServiceImpl : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly ITagRepository _tagRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IUserRepository _userRepository;


    public PostServiceImpl(IPostRepository postRepository,
        ITagRepository tagRepository,
        IAuthorRepository authorRepository, 
        IUserRepository userRepository)
    {
        _postRepository = postRepository;
        _tagRepository = tagRepository;
        _authorRepository = authorRepository;
        _userRepository = userRepository;
    }

    public async Task<PostPagedListDto> GetPosts(PostFilterDto postFilterDto)
    {
        var count = await _postRepository.GetPostCount(postFilterDto);
        if (count == 0)
        {
            throw new NotFoundException("Posts not found");
        }

        var posts = await _postRepository.GetPosts(postFilterDto, postFilterDto.page, count);
        return null;
    }

    public async Task<PostFullDto> GetPostById(Guid id)
    {
        var post = await _postRepository.GetPostById(id);
        if (post == null)
        {
            throw new NotFoundException("Post not found");
        }

        return new PostFullDto(post);
    }

    public async Task<Guid> CreatePost(CreatePostDto createPostDto, List<Guid> tagIds, Guid userId)
    {
        var user = await _userRepository.GetUserById(userId);
        var author = await _authorRepository.GetAuthorByUserId(userId);
    
        if (author == null)
        {
            var newAuthor = new AuthorEntity(user);
            await _authorRepository.CreateAuthor(newAuthor);

            user.Author = newAuthor; 
            await _userRepository.UpdateUser(user);
            author = newAuthor; 
        }
    
        var postEntity = new PostEntity
        {
            title = createPostDto.title,
            description = createPostDto.description,
            readingTime = createPostDto.readTime,
            image = createPostDto.image,
            authorId = author.Id,  
            author = author.FullName,
        };
        Console.WriteLine(postEntity.authorId);
        // var tags = await _tagRepository.GetTagsByIds(tagIds);
        // postEntity.tags = tags;
        
        var postId = await _postRepository.CreatePost(postEntity);

        return postId;
    }


   
    // public async Task LikePost(Guid idPost, Guid idUser)
    // {
    //     var post = await _postRepository.GetPostById(idPost);
    //     if (post == null)
    //     {
    //         throw new NotFoundException("Post not found");
    //     }
    //
    //     var user = await _userRepository.GetUserById(idUser);
    //     if (user == null)
    //     {
    //         throw new NotFoundException("User not found");
    //     }
    //
    //     await _postRepository.LikePost(idPost, idUser);
    // }
    //
    // public async Task deleteLikePost(Guid idPost, Guid idUser)
    // {
    //     var post = await _postRepository.GetPostById(idPost);
    //     if (post == null)
    //     {
    //         throw new NotFoundException("Post not found");
    //     }
    //
    //     var user = await _userRepository.GetUserById(idUser);
    //     if (user == null)
    //     {
    //         throw new NotFoundException("User not found");
    //     }
    //
    //     await _postRepository.deleteLikePost(idPost, idUser);
    // }
}