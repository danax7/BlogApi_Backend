using BlogApi.DTO.PostDTO;
using BlogApi.Entity;
using BlogApi.Entity.Enums;

namespace BlogApi.Repository.Interface;

public interface ICommunityRepository
{
    Task<List<CommunityEntity>> GetCommunityList();

    Task<List<CommunityEntity>> GetMyCommunityList(Guid userId);

    //Todo: вытягиавть id пользователя еще на уровне контроллера и передавать его в сервис
    Task<CommunityEntity> GetCommunity(Guid id);
    // Task<List<PostDto>> GetCommunityPostList(Guid id, PostFilterDto postFilterDto);
    // Task CreatePost(Guid id, CreatePostDto postCreateDto);
    // Task<CommunityRole> GetCommunityRoleList(Guid id);
    // Task Subscribe(Guid id);
    // Task Unsubscribe(Guid id);
    //
}