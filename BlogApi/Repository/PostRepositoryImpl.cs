using BlogApi.DTO.PostDTO;
using BlogApi.Entity;
using BlogApi.Exception;
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

        // if (postFilterDto.tags != null && postFilterDto.tags.Any())
        // {
        //     query = query.Where(post => post.tags.Any(tag => postFilterDto.tags.Contains(tag)));
        // }

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
        return _context.Posts.FirstOrDefaultAsync(post => post.id == id);
    }

    public Task<List<PostEntity>> GetPosts(PostFilterDto postFilterDto, int start, int count)
    {
        var query = _context.Posts.AsQueryable();

        // if (postFilterDto.tags != null && postFilterDto.tags.Any())
        // {
        //     query = query.Where(post => post.tags.Any(tag => postFilterDto.tags.Contains(tag)));
        // }

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
    
    public async Task<Guid> CreatePost(PostEntity postEntity)
    {
        _context.Posts.Add(postEntity);

        foreach (var tag in postEntity.tags)
        {
            _context.Attach(tag);
            _context.PostTags.Add(new PostTagsEntity(postEntity.id, tag.Id));
        }
        
        await _context.SaveChangesAsync();
        return postEntity.id;
    }
    
    public async Task LikePost(Guid postId, Guid userId)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(p => p.id == postId);
        if (post == null)
        {
            throw new NotFoundException("Post not found");
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
        {
            throw new NotFoundException("User not found");
        }
        
        var postLike = await _context.Likes.FirstOrDefaultAsync(pl => pl.PostId == postId && pl.UserId == userId);
        if (postLike != null)
        {
            throw new BadRequestException("Post already liked");
        }

        _context.Likes.Add(new LikeEntity(userId, postId));
        await _context.SaveChangesAsync();
    }

    public async Task DeletePostLike(Guid postId, Guid userId)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(p => p.id == postId);
        if (post == null)
        {
            throw new NotFoundException("Post not found");
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
        {
            throw new NotFoundException("User not found");
        }

        var postLike = await _context.Likes.FirstOrDefaultAsync(pl => pl.PostId == postId && pl.UserId == userId);
        if (postLike == null)
        {
            throw new BadRequestException("Post not liked");
        }

        _context.Likes.Remove(postLike);
        await _context.SaveChangesAsync();
    }


}