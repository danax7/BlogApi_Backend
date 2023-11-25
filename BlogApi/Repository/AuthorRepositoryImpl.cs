using BlogApi.Entity;
using BlogApi.Exception;
using BlogApi.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Repository;

public class AuthorRepositoryImpl : IAuthorRepository
{
    private readonly BlogDbContext _context;


    public AuthorRepositoryImpl(BlogDbContext blogContext)
    {
        _context = blogContext;
    }

    public async Task<List<AuthorEntity>> GetAuthorList()
    {
        return await _context.Authors.ToListAsync();
    }

    public async Task<AuthorEntity> GetAuthorById(Guid authorId)
    {
        var author = await _context.Authors.FirstOrDefaultAsync(author => author.Id == authorId);
        if (author == null)
        {
            throw new NotFoundException("Author not found");
        }

        return author;
    }
    
    public async Task<AuthorEntity> GetAuthorByUserId(Guid userId)
    {
        var author = await _context.Authors.FirstOrDefaultAsync(author => author.UserId == userId);
        // if (author == null)
        // {
        //     throw new NotFoundException("Author not found");
        // }

        return author;
    }

    public async Task CreateAuthor(AuthorEntity author)
    {
        _context.Authors.Add(author);
        await _context.SaveChangesAsync();
    }
}