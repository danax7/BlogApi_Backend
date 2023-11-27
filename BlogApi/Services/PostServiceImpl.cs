using BlogApi.DTO;
using BlogApi.DTO.PostDTO;
using BlogApi.Entity;
using BlogApi.Exception;
using BlogApi.Helpers;
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

        var maxPageSize = 10;
        var PageInfoEntity = PageInfoCalculator.GetPageInfoDto(maxPageSize, postFilterDto.page, count);
        var skipCount = (postFilterDto.page - 1) * maxPageSize;
        if (postFilterDto.page > PageInfoEntity.count || postFilterDto.page < 1)
        {
            throw new BadRequestException("Page out of range");
        }

        var posts = await _postRepository.GetPosts(postFilterDto, skipCount, count);
        var postsDto = posts.Select(post => new PostDto(post)).ToArray();

        var postPagedListDto = new PostPagedListDto
        {
            posts = postsDto,
            pagination = PageInfoEntity,
        };

        return postPagedListDto;
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
            createTime = DateTime.Now,
            title = createPostDto.title,
            description = createPostDto.description,
            readingTime = createPostDto.readTime,
            image = createPostDto.image,
            authorId = author.Id,
            author = author.FullName,
            tags = await _tagRepository.GetTagsByIds(tagIds)
        };
        Console.WriteLine(postEntity.authorId);
        var tags = await _tagRepository.GetTagsByIds(tagIds);
        postEntity.tags = tags;
        //TODO: Add tags

        var postId = await _postRepository.CreatePost(postEntity);

        return postId;
    }


    public async Task AddLike(Guid idPost, Guid idUser)
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

        var author = await _authorRepository.GetAuthorById(post.authorId);
        author.AddLike();
        
        await _authorRepository.UpdateAuthor(author);
        await _postRepository.LikePost(idPost, idUser);
    }

    public async Task RemoveLike(Guid idPost, Guid idUser)
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

        var author = await _authorRepository.GetAuthorById(post.authorId);
        author.RemoveLike();
        
        await _authorRepository.UpdateAuthor(author);
        await _postRepository.DeletePostLike(idPost, idUser);
    }
}