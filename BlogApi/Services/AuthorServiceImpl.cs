using BlogApi.DTO.AuthorDTO;
using BlogApi.Entity;
using BlogApi.Repository.Interface;
using BlogApi.Services.Interface;

namespace BlogApi.Services;

public class AuthorServiceImpl : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IUserService _userService;

    public AuthorServiceImpl(IAuthorRepository authorRepository, IUserService userService)
    {
        _authorRepository = authorRepository;
        _userService = userService;
    }

    public async Task<List<AuthorDto>> GetAuthorList()
    {
        var authors = await _authorRepository.GetAuthorList();
        var authorDtos = authors.Select(author => new AuthorDto(author)).ToList();
        if (authorDtos.Count == 0)
        {
            throw new System.Exception("Authors not found");
        }
        return authorDtos;
    }
    
    public async Task<AuthorEntity> GetAuthorById(Guid authorId)
    {
        var author = await _authorRepository.GetAuthorById(authorId);
        return author;
    } 
    
    public async Task CreateAuthor(Guid userId)
    {
        var user = await _userService.GetUserProfile(userId);
        var author = new AuthorEntity(user);
        
        await _authorRepository.CreateAuthor(author);
    }
    
}