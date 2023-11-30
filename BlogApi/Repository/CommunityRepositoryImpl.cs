using BlogApi.Entity;
using BlogApi.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Repository;

public class CommunityRepositoryImpl : ICommunityRepository
{
    private readonly BlogDbContext _context;

    public CommunityRepositoryImpl(BlogDbContext context)
    {
        _context = context;
    }

    public async Task<List<CommunityEntity>> GetCommunityList()
    {
        return await _context.Communities.ToListAsync();
    }

    public async Task<List<CommunityEntity>> GetMyCommunityList(Guid userId)
    {
        var userCommunities = await _context.UserCommunities
            .Where(uc => uc.UserId == userId)
            .ToListAsync();

        var communityIds = userCommunities.Select(uc => uc.CommunityId).ToList();
        var communities = await _context.Communities
            .Where(c => communityIds.Contains(c.id))
            .ToListAsync();

        return communities;
    }

    public async Task<CommunityEntity> GetCommunity(Guid Id)
    {
        return await _context.Communities.FirstOrDefaultAsync(c => c.id == Id);
    }
    
    public async Task CreateCommunity(CommunityEntity community)
    {
        await _context.Communities.AddAsync(community);
        await _context.SaveChangesAsync();
    }
    

    // public async Task<List<PostEntity>> GetCommunityPostList(Guid id, PostFilterDto postFilterDto)
    // {
    //     var posts = await _context.Posts
    //         .Where(p => p.CommunityId == id)
    //         .ToListAsync();
    //
    //     if (postFilterDto.SortBy == SortBy.Date)
    //     {
    //         posts = postFilterDto.SortDirection == SortDirection.Asc
    //             ? posts.OrderBy(p => p.CreatedAt).ToList()
    //             : posts.OrderByDescending(p => p.CreatedAt).ToList();
    //     }
    //     else if (postFilterDto.SortBy == SortBy.Likes)
    //     {
    //         posts = postFilterDto.SortDirection == SortDirection.Asc
    //             ? posts.OrderBy(p => p.Likes).ToList()
    //             : posts.OrderByDescending(p => p.Likes).ToList();
    //     }
    //     else if (postFilterDto.SortBy == SortBy.Comments)
    //     {
    //         posts = postFilterDto.SortDirection == SortDirection.Asc
    //             ? posts.OrderBy(p => p.Comments).ToList()
    //             : posts.OrderByDescending(p => p.Comments).ToList();
    //     }
    //
    //     return posts;
    // }
}