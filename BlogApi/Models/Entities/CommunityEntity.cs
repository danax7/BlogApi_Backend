using System.ComponentModel.DataAnnotations;

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
}