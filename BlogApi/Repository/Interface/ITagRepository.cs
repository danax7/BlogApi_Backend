using BlogApi.Entity;

namespace BlogApi.Repository.Interface;

public interface ITagRepository
{
    Task<List<TagEntity>> GetTags();
    Task<TagEntity> GetTagById(Guid id);
    Task<List<TagEntity>> GetTagsByIds(List<Guid> ids);
}