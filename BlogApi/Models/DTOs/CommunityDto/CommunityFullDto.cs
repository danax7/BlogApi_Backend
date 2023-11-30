using System.ComponentModel.DataAnnotations;
using BlogApi.Entity;
using BlogApi.Entity.Enums;

namespace BlogApi.DTO.CommunityDto;

public class CommunityFullDto
{
    [Required] public Guid id { get; set; }
    [Required] public DateTime createTime { get; set; }
    [Required] [MinLength(1)] public string name { get; set; }
    public string description { get; set; }
    [Required] public bool isClosed { get; set; }
    [Required] public Int32 subscribersCount { get; set; }
    [Required] public List<UserDto> administrators { get; set; }


    public CommunityFullDto()
    {
    }

    public CommunityFullDto(CommunityEntity communityEntity, List<UserEntity> admins)
    {
        id = communityEntity.id;
        createTime = communityEntity.createTime;
        name = communityEntity.name;
        description = communityEntity.description;
        isClosed = communityEntity.isClosed;
        subscribersCount = communityEntity.subscribersCount;
        administrators = admins.Select(x => new UserDto(x)).ToList();
        //Тут возникает ошибка, если вывести список администраторов
        // administrators = communityEntity.UserCommunities
        //     .Where(userCommunity => userCommunity.Role == CommunityRole.Administrator)
        //     .Select(userCommunity => new UserDto(userCommunity.User))
        //     .ToList();
    }
}