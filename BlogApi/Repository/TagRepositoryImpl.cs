using BlogApi.Entity;
using BlogApi.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Repository;

public class TagRepositoryImpl : ITagRepository
{
    private readonly BlogDbContext _blogContext;
    
    public TagRepositoryImpl(BlogDbContext blogContext)
    {
        _blogContext = blogContext;
    }
    
    public async Task<List<TagEntity>> GetTags()
    {
        return await _blogContext.Tags.ToListAsync();
    }
    
    
    
    
}