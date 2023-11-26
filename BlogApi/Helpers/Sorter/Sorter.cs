using BlogApi.Entity;
using BlogApi.Entity.Enums;

namespace BlogApi.Helpers.Sorter;

public class Sorter
{
    private readonly Dictionary<SortType, ISortStrategy> _sortStrategies;

    public Sorter()
    {
        _sortStrategies = new Dictionary<SortType, ISortStrategy>
        {
            { SortType.CreateDesc, new SortingStrategies.CreateDescSortStrategy() },
            { SortType.CreateAsc, new SortingStrategies.CreateAscSortStrategy() },
            // { SortType.LikeAsc, new SortingStrategies.LikeAscSortStrategy() },
            // { SortType.LikeDesc, new SortingStrategies.LikeDescSortStrategy() }
        };
    }

    public IQueryable<PostEntity> ApplySorting(IQueryable<PostEntity> query, SortType sortType)
    {
        if (_sortStrategies.TryGetValue(sortType, out var sortStrategy))
        {
            return sortStrategy.ApplySorting(query);
        }

        return query.OrderByDescending(post => post.createTime);
    }
}