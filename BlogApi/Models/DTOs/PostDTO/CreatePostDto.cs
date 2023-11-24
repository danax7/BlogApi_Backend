using System.ComponentModel.DataAnnotations;
using BlogApi.Entity;

namespace BlogApi.DTO.PostDTO;

public class CreatePostDto
{
    [Required] [MinLength(5)] public string title { get; set; }
    [Required] [MinLength(5)] public string description { get; set; }
    [Required] public Int32 readTime { get; set; }
    public string image { get; set; }
    public string addressId { get; set; }

    [Required] [MaxLength(1)] public List<TagDto.TagDto> tags { get; set; }
    //Todo: Check tags
}