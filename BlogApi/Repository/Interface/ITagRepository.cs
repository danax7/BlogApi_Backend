using BlogApi.Entity;

namespace BlogApi.Repository.Interface;

public interface ITagRepository
{
    // Task CreateTag(TagEntity tagEntity);
    // Task<TagEntity?> GetTag(String tag);
    Task<List<TagEntity>> GetTags();
    // Task UpdateTag(TagEntity tagEntity);
    // Task DeleteTag(TagEntity tagEntity);
    
}