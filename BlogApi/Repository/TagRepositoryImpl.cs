using BlogApi.Entity;
using BlogApi.Exception;
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

    public async Task<TagEntity> GetTagById(Guid id)
    {
        var tag = await _сontext.Tags.FirstOrDefaultAsync(tag => tag.Id == id);
        if (tag == null)
        {
            throw new NotFoundException($"Tag with id {id} not found");
        }

        return tag;
    }

    public async Task<List<TagEntity>> GetTagsByIds(List<Guid> ids)
    {
        return await _сontext.Tags.Where(tag => ids.Contains(tag.Id)).ToListAsync();
    }
}