using BlogApi.DTO.CommunityDto;
using BlogApi.DTO.PostDTO;
using BlogApi.Entity;
using BlogApi.Entity.Enums;
using BlogApi.Repository.Interface;
using BlogApi.Services.Interface;
using System.Linq;

namespace BlogApi.Services;

public class CommunityServiceImpl : ICommunityService
{
    private readonly ICommunityRepository _communityRepository;
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;


    public CommunityServiceImpl(
        ICommunityRepository communityRepository,
        IPostRepository postRepository, IUserRepository userRepository)

    {
        _communityRepository = communityRepository;
        _postRepository = postRepository;
        _userRepository = userRepository;
    }

    public async Task<List<CommunityDto>> GetCommunityList()
    {
        var communities = await _communityRepository.GetCommunityList();
        return communities.Select(x => new CommunityDto(x)).ToList();
    }

    public async Task<List<CommunityUserDto>> GetMyCommunityList(Guid userId)
    {
        var communities = await _communityRepository.GetMyCommunityList(userId);
        return communities.Select(x => new CommunityUserDto(x)).ToList();
    }

    public async Task<CommunityFullDto> GetCommunity(Guid id)
    {
        var community = await _communityRepository.GetCommunity(id);
        return new CommunityFullDto(community);
    }

    // public async Task<List<PostDto>> GetCommunityPostList(Guid id, PostFilterDto postFilterDto)
    // {
    //     // var posts = await _postRepository.GetCommunityPostList(id, postFilterDto);
    //     // return posts.Select(x => new PostDto(x)).ToList();
    //     return null;
    //     //TODO: check
    // }

    public async Task Subscribe(Guid userId, Guid communityId)
    {
        var user = await _userRepository.GetUserById(userId);
        var community = await _communityRepository.GetCommunity(communityId);

        if (user == null || community == null)
        {
            throw new System.Exception("User or community not found.");
        }

        if (!user.UserCommunities.Any(uc => uc.CommunityId == communityId))
        {
            user.UserCommunities.Add(new UserCommunityEntity { UserId = userId, CommunityId = communityId, Role = CommunityRole.Subscriber });
            await _userRepository.UpdateUser(user);
        }
    }

    public async Task Unsubscribe(Guid userId, Guid communityId)
    {
        var user = await _userRepository.GetUserById(userId);
        var community = await _communityRepository.GetCommunity(communityId);

        if (user == null || community == null)
        {
            throw new System.Exception("User or community not found.");
        }

      
        var userCommunity = user.UserCommunities.FirstOrDefault(uc => uc.CommunityId == communityId);
        if (userCommunity != null)
        {
            user.UserCommunities.Remove(userCommunity);
            await _userRepository.UpdateUser(user);
        }
      
    }
}