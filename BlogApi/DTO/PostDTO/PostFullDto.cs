using System.ComponentModel.DataAnnotations;
using BlogApi.Entity;

namespace BlogApi.DTO.PostDTO;

public class PostFullDto
{
    [Required] public Guid id { get; set; }
    [Required] public DateTime createTime { get; set; }
    [Required] public string title { get; set; }
    [Required] public string description { get; set; }
    [Required] public int readingTime { get; set; }
    public string image { get; set; }
    [Required] public string authorId { get; set; }
    [Required] public string author { get; set; }
    public string communityId { get; set; }
    public string communityName { get; set; }
    public Guid addressId { get; set; }
    [Required] public int likes { get; set; }
    [Required] public bool hasLike { get; set; }
    [Required] public int commentsCount { get; set; }
    //TODO: Check tags
    public List<TagEntity> tags { get; set; }
    //TODO: Check comments
    [Required] public List<CommentEntity> comments { get; set; }
}