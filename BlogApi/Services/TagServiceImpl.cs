using BlogApi.DTO.TagDto;
using BlogApi.Entity;
using BlogApi.Exception;
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

    public async Task<List<TagDto>> GetTags()
    {
        var tags = await _tagRepository.GetTags();
        var tagList = tags.Select(tagEntity => new TagDto(tagEntity)).ToList();
        return tagList;
    }
}