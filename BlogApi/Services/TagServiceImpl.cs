using BlogApi.Entity;
using BlogApi.Repository.Interface;
using BlogApi.Services.Interface;

namespace BlogApi.Services;

public class TagServiceImpl : ITagService
{
    private readonly ITagRepository _tagRepository;

    public TagServiceImpl(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public async Task<List<TagEntity>> GetTags()
    {
        return await _tagRepository.GetTags();
    }
}