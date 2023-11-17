using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BlogApi.Entity;

namespace BlogApi.DTO.PostDTO;

public class PostDto
{
    [Required] public Guid id { get; set; }
    [Required] public DateTime createTime { get; set; }
    [Required] [MinLength(1)] public string title { get; set; }
    [Required] [MinLength(1)] public string description { get; set; }
    [Required] public int readingTime { get; set; }
    public string image { get; set; }
    [Required] public string authorId { get; set; }
    [Required] [MinLength(1)] public string author { get; set; }
    public string communityId { get; set; }
    public string communityName { get; set; }
    public Guid addressId { get; set; }
    [Required] public int likes { get; set; }
    [Required] [DefaultValue(false)] public bool hasLike { get; set; }
    [Required] [DefaultValue(0)] public int commentsCount { get; set; }
    public List<TagDto.TagDto> tags { get; set; }
}