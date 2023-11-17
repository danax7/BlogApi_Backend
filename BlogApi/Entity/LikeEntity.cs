namespace BlogApi.Entity;

public class LikeEntity
{
    public Guid UserId { get; set; }
    public Guid PostId { get; set; }
    public UserEntity User { get; set; }
    public PostEntity Post { get; set; }
}