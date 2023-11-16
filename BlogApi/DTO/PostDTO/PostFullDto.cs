using System.ComponentModel.DataAnnotations;

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
    //TODO: Add tags
    // public ICollection<TagEntity> Tags { get; set; }
    //TODO: Add comments
    //[Required] public 
}