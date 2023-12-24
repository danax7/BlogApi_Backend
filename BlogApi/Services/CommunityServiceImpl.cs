using BlogApi.DTO.CommunityDto;
using BlogApi.DTO.PostDTO;
using BlogApi.Entity;
using BlogApi.Entity.Enums;
using BlogApi.Repository.Interface;
using BlogApi.Services.Interface;
using System.Linq;
using BlogApi.Exception;
using Microsoft.AspNetCore.Mvc;

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
        if (communities == null)
        {
            throw new NotFoundException("Communities not found");
        }

        var comminityList = communities.Select(x => new CommunityUserDto(x)).ToList();
        return comminityList;
    }

    public async Task<CommunityFullDto> GetCommunity(Guid id)
    {
        var community = await _communityRepository.GetCommunity(id);
        if (community == null)
        {
            throw new NotFoundException("Community not found.");
        }

        var admins = await _communityRepository.GetCommunityAdmins(id);
        return new CommunityFullDto(community, admins);
    }

    public async Task CreateCommunity(Guid userId, CreateCommunityDto communityCreateDto)
    {
        var user = await _userRepository.GetUserById(userId);
        var community = new CommunityEntity
        {
            name = communityCreateDto.name,
            createTime = DateTime.Now,
            description = communityCreateDto.description,
            isClosed = communityCreateDto.isClosed,
            UserCommunities = new List<UserCommunityEntity>
            {
                new UserCommunityEntity
                {
                    UserId = user.Id,
                    Role = CommunityRole.Administrator
                }
            }
        };

        await _communityRepository.CreateCommunity(community);
    }

    public async Task<ActionResult<string>> GetGreatestUserCommunityRole(Guid userId, Guid communityId)
    {
        var user = await _userRepository.GetUserById(userId);
        var community = await _communityRepository.GetCommunity(communityId);

        if (user == null || community == null)
        {
            throw new NotFoundException("User or community not found.");
        }

        var userCommunity = user.UserCommunities.FirstOrDefault(uc => uc.CommunityId == communityId);
        if (userCommunity == null)
        {
            throw new NotFoundException("User is not associated with the community.");
        }

        return Enum.GetName(typeof(CommunityRole), userCommunity.Role);
    }

    public async Task Subscribe(Guid userId, Guid communityId)
    {
        var user = await _userRepository.GetUserById(userId);
        var community = await _communityRepository.GetCommunity(communityId);

        if (user == null || community == null)
        {
            throw new NotFoundException("User or community not found.");
        }

        if (user.UserCommunities.Any(uc => uc.CommunityId == communityId))
        {
            throw new BadRequestException("User is already subscribed to the community.");
        }
        //TODO: check

        var userCommunity = user.UserCommunities.FirstOrDefault(uc => uc.CommunityId == communityId);
        if (userCommunity == null)
        {
            user.UserCommunities.Add(new UserCommunityEntity
            {
                UserId = user.Id,
                CommunityId = communityId,
                Role = CommunityRole.Subscriber,
            });
            community.IncrementSubscribersCount();
            await _communityRepository.UpdateCommunity(community);
            await _userRepository.UpdateUser(user);
        }
    }

    public async Task Unsubscribe(Guid userId, Guid communityId)
    {
        var user = await _userRepository.GetUserById(userId);
        var community = await _communityRepository.GetCommunity(communityId);

        if (user == null || community == null)
        {
            throw new NotFoundException("User or community not found.");
        }

        var userCommunity = user.UserCommunities.FirstOrDefault(uc => uc.CommunityId == communityId);
        if (userCommunity.Role == CommunityRole.Administrator)
        {
            throw new BadRequestException("Administrator cannot unsubscribe from the community.");
        }

        if (userCommunity != null && userCommunity.Role == CommunityRole.Subscriber)
        {
            user.UserCommunities.Remove(userCommunity);
            community.DecrementSubscribersCount();
            await _communityRepository.UpdateCommunity(community);
            await _userRepository.UpdateUser(user);
        }
    }
}