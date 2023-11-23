using BlogApi.DTO.CommunityDto;
using BlogApi.DTO.PostDTO;
using BlogApi.Entity.Enums;

namespace BlogApi.Services.Interface;

public interface ICommunityService
{
    Task<List<CommunityDto>> GetCommunityList();
    Task<List<CommunityUserDto>> GetMyCommunityList(Guid userId);
    Task<CommunityFullDto> GetCommunity(Guid id);
    //Task<List<PostDto>> GetCommunityPostList(Guid id, PostFilterDto postFilterDto);
    // Task CreatePost(Guid id, CreatePostDto postCreateDto);
    // Task<CommunityRole> GetCommunityRoleList(Guid id);
    Task Subscribe(Guid userId, Guid communityId);
    Task Unsubscribe(Guid id, Guid communityId);
}