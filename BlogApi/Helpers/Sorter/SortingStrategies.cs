using BlogApi.Entity;

namespace BlogApi.Helpers.Sorter;

public class SortingStrategies
{
    public class CreateDescSortStrategy : ISortStrategy
    {
        public IOrderedQueryable<PostEntity> ApplySorting(IQueryable<PostEntity> query)
        {
            return query.OrderByDescending(post => post.createTime);
        }
    }

    public class CreateAscSortStrategy : ISortStrategy
    {
        public IOrderedQueryable<PostEntity> ApplySorting(IQueryable<PostEntity> query)
        {
            return query.OrderBy(post => post.createTime);
        }
    }

    public class LikeAscSortStrategy : ISortStrategy
    {
        public IOrderedQueryable<PostEntity> ApplySorting(IQueryable<PostEntity> query)
        {
            return query.OrderBy(post => post.likesCount);
        }
    }

    public class LikeDescSortStrategy : ISortStrategy
    {
        public IOrderedQueryable<PostEntity> ApplySorting(IQueryable<PostEntity> query)
        {
            return query.OrderByDescending(post => post.likesCount);
        }
    }
}