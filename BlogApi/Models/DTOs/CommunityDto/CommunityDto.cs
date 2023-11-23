using System.ComponentModel.DataAnnotations;
using BlogApi.Entity;

namespace BlogApi.DTO.CommunityDto;

public class CommunityDto
{
    [Required] public Guid id { get; set; }
    [Required] public DateTime createTime { get; set; }
    [Required] [MinLength(1)] public string name { get; set; }
    public string description { get; set; }
    [Required] public bool isClosed { get; set; }
    [Required] public Int32 subscribersCount { get; set; }
    
    
    public CommunityDto()
    {
    }
    
    public CommunityDto(CommunityEntity communityEntity)
    {
        id = communityEntity.id;
        createTime = communityEntity.createTime;
        name = communityEntity.name;
        description = communityEntity.description;
        isClosed = communityEntity.isClosed;
        subscribersCount = communityEntity.subscribersCount;
    }
}