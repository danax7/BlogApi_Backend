namespace BlogApi.Entity;

public class LikeEntity
{
    public Guid UserId { get; set; }
    public Guid PostId { get; set; }
    public UserEntity User { get; set; }
    public PostEntity Post { get; set; }

    public LikeEntity(Guid userId, Guid postId)
    {
        UserId = userId;
        PostId = postId;
    }

    public LikeEntity()
    {
    }
}