using BlogApi.Entity.Enums;

namespace BlogApi.Entity;

public class UserCommunityEntity
{
    public Guid UserId { get; set; }
    public UserEntity User { get; set; }

    public Guid CommunityId { get; set; }
    public CommunityEntity Community { get; set; }
    
    public CommunityRole Role { get; set; }
}