using BlogApi.Entity;

namespace BlogApi.Helpers.Sorter;

public interface ISortStrategy
{
    IOrderedQueryable<PostEntity> ApplySorting(IQueryable<PostEntity> query);
}