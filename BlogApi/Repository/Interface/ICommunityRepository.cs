using BlogApi.DTO.PostDTO;
using BlogApi.Entity;
using BlogApi.Entity.Enums;

namespace BlogApi.Repository.Interface;

public interface ICommunityRepository
{
    Task<List<CommunityEntity>> GetCommunityList();
    Task<List<CommunityEntity>> GetMyCommunityList(Guid userId);
    Task<CommunityEntity> GetCommunity(Guid id);
    Task<List<UserEntity>> GetCommunityAdmins(Guid id);
    Task CreateCommunity(CommunityEntity community);
    Task UpdateCommunity(CommunityEntity community);
    Task<List<CommunityEntity>> GetCommunitiesByUserId(Guid? userId);
}