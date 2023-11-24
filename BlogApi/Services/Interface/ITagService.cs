using BlogApi.Entity;

namespace BlogApi.Services.Interface;

public interface ITagService
{
    Task<List<TagEntity>> GetTags();
    Task<TagEntity> GetTagById(Guid id);
}