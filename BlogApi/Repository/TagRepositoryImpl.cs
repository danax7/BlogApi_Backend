using BlogApi.Entity;
using BlogApi.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Repository;

public class TagRepositoryImpl : ITagRepository
{
    private readonly BlogDbContext _сontext;

    public TagRepositoryImpl(BlogDbContext blogContext)
    {
        _сontext = blogContext;
    }

    public async Task<List<TagEntity>> GetTags()
    {
        return await _сontext.Tags.ToListAsync();
    }
}