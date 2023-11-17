using System.ComponentModel.DataAnnotations;

namespace BlogApi.DTO.TagDto;

public class TagDto
{
    public Guid id { get; set; }
    [Required] public DateTime createTime { get; set; }
    [MinLength(1)] [Required] public string name { get; set; }
    
}