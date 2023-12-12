using System.ComponentModel.DataAnnotations;
using BlogApi.DTO.CommunityDto;

namespace BlogApi.Entity;

public class CommunityEntity
{
    public Guid id { get; set; }
    public DateTime createTime { get; set; }
    [Required] public string name { get; set; }
    [Required] public string description { get; set; }
    [Required] public bool isClosed { get; set; }
    [Required] public Int32 subscribersCount { get; set; }

    public ICollection<UserCommunityEntity> UserCommunities { get; set; }


    public CommunityEntity()
    {
    }

    public void IncrementSubscribersCount()
    {
        subscribersCount++;
    }

    public void DecrementSubscribersCount()
    {
        subscribersCount--;
    }

    public CommunityEntity(CreateCommunityDto communityCreateDto)
    {
        id = Guid.NewGuid();
        createTime = DateTime.Now;
        name = communityCreateDto.name;
        description = communityCreateDto.description;
        isClosed = communityCreateDto.isClosed;
    }
}