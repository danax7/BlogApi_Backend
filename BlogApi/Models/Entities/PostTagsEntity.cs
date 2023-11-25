namespace BlogApi.Entity;

public class PostTagsEntity
{
    public Guid PostId { get; set; }
    public Guid TagId { get; set; }
    
    public PostEntity Post { get; set; }
    public TagEntity Tag { get; set; }
    public PostTagsEntity(Guid postId, Guid tagId)
    {
        PostId = postId;
        TagId = tagId;
    }
}