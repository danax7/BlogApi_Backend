using BlogApi.DTO.CommunityDto;
using BlogApi.DTO.PostDTO;
using BlogApi.Repository.Interface;
using BlogApi.Services.Interface;

namespace BlogApi.Services;

public class CommunityServiceImpl : ICommunityService
{
    private readonly ICommunityRepository _communityRepository;
    private readonly IPostRepository _postRepository;


    public CommunityServiceImpl(
        ICommunityRepository communityRepository,
        IPostRepository postRepository)

    {
        _communityRepository = communityRepository;
        _postRepository = postRepository;
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
}