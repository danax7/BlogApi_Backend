using BlogApi.Entity;
using BlogApi.Entity.Enums;
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

    public async Task<List<UserEntity>> GetCommunityAdmins(Guid id)
    {
        var userCommunities = await _context.UserCommunities
            .Where(uc => uc.CommunityId == id && uc.Role == CommunityRole.Administrator)
            .ToListAsync();

        var userIds = userCommunities.Select(uc => uc.UserId).ToList();
        var users = await _context.Users
            .Where(u => userIds.Contains(u.Id))
            .ToListAsync();

        return users;
    }

    public async Task<List<CommunityEntity>> GetCommunitiesByUserId(Guid? userId)
    {
        return await _context.UserCommunities
            .Where(uc => uc.UserId == userId)
            .Select(uc => uc.Community)
            .ToListAsync();
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
}