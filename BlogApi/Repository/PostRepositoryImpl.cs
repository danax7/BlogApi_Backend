using BlogApi.DTO.PostDTO;
using BlogApi.Entity;
using BlogApi.Exception;
using BlogApi.Helpers;
using BlogApi.Helpers.Sorter;
using BlogApi.Repository.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Repository;

public class PostRepositoryImpl : IPostRepository
{
    private readonly BlogDbContext _context;

    public PostRepositoryImpl(BlogDbContext context)
    {
        _context = context;
    }

    public async Task<Int32> GetPostCount(PostFilterDto postFilterDto, Guid? userId)
    {
        var query = _context.Posts.Include(post => post.tags).AsQueryable();

        if (postFilterDto.tags != null && postFilterDto.tags.Any())
        {
            query = query.Where(post => post.tags.Any(tag => postFilterDto.tags.Contains(tag.Id)));
        }
        if (postFilterDto.author != null)
        {
            query = query.Where(post => postFilterDto.author == post.author);
        }
        
        if (postFilterDto.onlyMyCommunities != null && postFilterDto.onlyMyCommunities.Value && postFilterDto.onlyMyCommunities == true && userId != null)
        {
            var userCommunities = await _context.UserCommunities
                .Where(uc => uc.UserId == userId)
                .Select(uc => uc.CommunityId)
                .ToListAsync();

            query = query.Where(post => userCommunities.Contains(post.communityId ?? Guid.Empty));
        }
        
        //TODO Check this
        if (userId == null)
        {
            // User is not authenticated, show posts from open communities and posts not associated with any community
            query = query.Where(post => post.Community.isClosed == false || post.communityId == null);
        }
        else
        {
            // User is authenticated, show posts from open communities, posts not associated with any community,
            // and posts from closed communities to which the user is subscribed
            var userCommunities = await _context.UserCommunities
                .Where(uc => uc.UserId == userId)
                .Select(uc => uc.CommunityId)
                .ToListAsync();

            query = query.Where(post =>
                post.Community.isClosed == false || post.communityId == null || userCommunities.Contains(post.communityId ?? Guid.Empty));
        }
        
        if (postFilterDto.minReadingTime != null)
        {
            query = query.Where(post => postFilterDto.minReadingTime <= post.readingTime);
        }
        
        if (postFilterDto.maxReadingTime != null)
        {
            query = query.Where(post => postFilterDto.maxReadingTime >= post.readingTime);
        }
        if (postFilterDto.onlyMyCommunities != null && postFilterDto.onlyMyCommunities.Value && postFilterDto.onlyMyCommunities == true && userId != null)
        {
            var userCommunities = await _context.UserCommunities
                .Where(uc => uc.UserId == userId)
                .Select(uc => uc.CommunityId)
                .ToListAsync();

            query = query.Where(post => userCommunities.Contains(post.communityId ?? Guid.Empty ));
        }
        return await query.CountAsync();
    }

    public Task<PostEntity?> GetPostById(Guid id)
    {
        return _context.Posts
            .Include(post => post.tags)
            .Include(post => post.Comments)
            .FirstOrDefaultAsync(post => post.id == id);
    }


    public async Task<List<PostEntity>> GetPosts(PostFilterDto postFilterDto, int start, int count, Guid? userId)
    {
        var query = _context.Posts
            .Include(post => post.tags)
            .Include(post => post.Likes).AsQueryable();
        
        //TODO Check this
        //Сортировка по тегам
        if (postFilterDto.tags != null && postFilterDto.tags.Any())
        {
            query = query.Where(post => post.tags.Any(tag => postFilterDto.tags.Contains(tag.Id)));
        }
        
        
        //TODO: Проверить, что пост не находится в приватном сообществе, в котором пользователь не состоит, а если состоит то отображать посты
    
        // Если пользователь не авторизован, отображаем только посты из открытых сообществ
        if (userId == null)
        {
            // User is not authenticated, show posts from open communities and posts not associated with any community
            query = query.Where(post => post.Community.isClosed == false || post.communityId == null);
        }
        
        else
        {
            // User is authenticated, show posts from open communities, posts not associated with any community,
            // and posts from closed communities to which the user is subscribed
            var userCommunities = await _context.UserCommunities
                .Where(uc => uc.UserId == userId)
                .Select(uc => uc.CommunityId)
                .ToListAsync();

            query = query.Where(post =>
                post.Community.isClosed == false || post.communityId == null || userCommunities.Contains(post.communityId ?? Guid.Empty));
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
        
        if (postFilterDto.onlyMyCommunities != null && postFilterDto.onlyMyCommunities.Value && userId != null)
        {
            var userCommunities = await _context.UserCommunities
                .Where(uc => uc.UserId == userId)
                .Select(uc => uc.CommunityId)
                .ToListAsync();

            query = query.Where(post => userCommunities.Contains(post.communityId ?? Guid.Empty ));
        }
        
        if (postFilterDto.sorting != null)
        {
            var sorter = new Sorter();
            query = sorter.ApplySorting(query, postFilterDto.sorting.Value);
        }

        return await query.Skip(start).Take(count).ToListAsync();
    }

    public async Task<Guid> CreatePost(PostEntity postEntity)
    {
        _context.Posts.Add(postEntity);
        
        foreach (var tag in postEntity.tags)
        {
            _context.Attach(tag);
            _context.PostTags.Add(new PostTagsEntity(postEntity.id, tag.Id));
            //TODO: Убрать здесь
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
        post.likesCount++;
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
        post.likesCount--;
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePost(PostEntity post)
    {
        _context.Entry(post).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}