using BlogApi.DTO.PostDTO;
using BlogApi.Entity;
using BlogApi.Helpers;
using BlogApi.Helpers.Sorter;
using BlogApi.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Repository;

public class PostRepositoryImpl : IPostRepository
{
    private readonly BlogDbContext _context;

    public PostRepositoryImpl(BlogDbContext context)
    {
        _context = context;
    }

    public Task<Int32> GetPostCount(PostFilterDto postFilterDto)
    {
        var query = _context.Posts.AsQueryable();

        if (postFilterDto.tags != null && postFilterDto.tags.Any())
        {
            query = query.Where(post => post.tags.Any(tag => postFilterDto.tags.Contains(tag)));
        }

        if (postFilterDto.author != null)
        {
            query = query.Where(post => postFilterDto.author == post.author);
        }

        if (postFilterDto.minReadingTime != null)
        {
            query = query.Where(post => postFilterDto.minReadingTime <= post.readingTime);
        }

        if (postFilterDto.maxReadingTime != null)
        {
            query = query.Where(post => postFilterDto.maxReadingTime >= post.readingTime);
        }

        return query.CountAsync();
    }

    public Task<PostEntity?> GetPostById(Guid id)
    {
        return _context.Posts.FirstOrDefaultAsync(dish => dish.id == id);
    }

    public Task<List<PostEntity>> GetPosts(PostFilterDto postFilterDto, int start, int count)
    {
        var query = _context.Posts.AsQueryable();

        if (postFilterDto.tags != null && postFilterDto.tags.Any())
        {
            query = query.Where(post => post.tags.Any(tag => postFilterDto.tags.Contains(tag)));
        }

        if (postFilterDto.author != null)
        {
            query = query.Where(post => postFilterDto.author == post.author);
        }

        if (postFilterDto.minReadingTime != null)
        {
            query = query.Where(post => postFilterDto.minReadingTime <= post.readingTime);
        }

        if (postFilterDto.maxReadingTime != null)
        {
            query = query.Where(post => postFilterDto.maxReadingTime >= post.readingTime);
        }

        // if (postFilterDto.onlyMyCommunities != null)
        // {
        //     query = query.Where(post => postFilterDto.onlyMyCommunities == post.);
        // }
        //
        //TODO: Check onlyMyCommunities
        if (postFilterDto.sorting != null)
        {
            var sorter = new Sorter();
            query = sorter.ApplySorting(query, postFilterDto.sorting.Value);
        }

        return query.Skip(start).Take(count).ToListAsync();
    }

    // public Task<List<PostEntity>> GetCommunityPostList(Guid id, PostFilterDto postFilterDto)
    // {
    //     var query = _context.Posts.AsQueryable();
    //
    //     if (postFilterDto.tags != null && postFilterDto.tags.Any())
    //     {
    //         query = query.Where(post => post.tags.Any(tag => postFilterDto.tags.Contains(tag)));
    //     }
    //
    //     if (postFilterDto.author != null)
    //     {
    //         query = query.Where(post => postFilterDto.author == post.author);
    //     }
    //
    //     if (postFilterDto.minReadingTime != null)
    //     {
    //         query = query.Where(post => postFilterDto.minReadingTime <= post.readingTime);
    //     }
    //
    //     if (postFilterDto.maxReadingTime != null)
    //     {
    //         query = query.Where(post => postFilterDto.maxReadingTime >= post.readingTime);
    //     }
    //     //TODO: Check GetCommunityPostList
    //     return null;
    // }
}