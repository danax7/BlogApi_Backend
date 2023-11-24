using BlogApi.Entity;

namespace BlogApi.Services.Interface;

public interface ITagService
{
    Task<List<TagEntity>> GetTags();
}