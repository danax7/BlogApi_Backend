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
    private readonly ICommentRepository _commentRepository;
    private readonly ICommunityRepository _communityRepository;


    public PostServiceImpl(IPostRepository postRepository,
        ITagRepository tagRepository,
        IAuthorRepository authorRepository,
        IUserRepository userRepository,
        ICommentRepository commentRepository,
        ICommunityRepository communityRepository)
    {
        _postRepository = postRepository;
        _tagRepository = tagRepository;
        _authorRepository = authorRepository;
        _userRepository = userRepository;
        _commentRepository = commentRepository;
        _communityRepository = communityRepository;
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
        var comments = await _commentRepository.GetAllFirstLevelPostCommentsById(id);
        if (post == null)
        {
            throw new NotFoundException("Post not found");
        }

        return new PostFullDto(post, comments);
    }

    public async Task<Guid> CreatePost(CreatePostDto createPostDto, List<Guid> tagIds, Guid userId)
    {
        var user = await _userRepository.GetUserById(userId);
        var author = await _authorRepository.GetAuthorByUserId(userId);
        
        var communityId = createPostDto.communityId ?? Guid.Empty;

        if (communityId != Guid.Empty || createPostDto.communityName != null)
        {
            var community = await _communityRepository.GetCommunity(communityId);
            createPostDto.communityName = community.name;
        }

        if (author == null)
        {
            var newAuthor = new AuthorEntity(user);
            await _authorRepository.CreateAuthor(newAuthor);

            user.Author = newAuthor;
            await _userRepository.UpdateUser(user);
            author = newAuthor;
        }
        var tags = await _tagRepository.GetTagsByIds(tagIds);
        if (tags.Count == 0)
        {
            throw new NotFoundException("One or more tags not found");
        }

        var postEntity = new PostEntity
        {
            createTime = DateTime.Now,
            title = createPostDto.title,
            description = createPostDto.description,
            readingTime = createPostDto.readTime,
            image = createPostDto.image,
            communityId = createPostDto.communityId,
            communityName = createPostDto.communityName,
            authorId = author.Id,
            author = author.FullName,
            tags = tags,
        };
        
        
        author.IncrementPostCount();
        await _authorRepository.UpdateAuthor(author);
        postEntity.tags = tags;

        var postId = await _postRepository.CreatePost(postEntity);

        // await _postRepository.UpdatePost(postEntity) ;

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