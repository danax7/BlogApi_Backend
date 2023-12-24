using BlogApi.DTO.TagDto;
using BlogApi.Entity;

namespace BlogApi.Services.Interface;

public interface ITagService
{
    Task<List<TagDto>> GetTags();
}