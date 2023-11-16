using System.ComponentModel.DataAnnotations;

namespace BlogApi.DTO.CommunityDto;

public class CommunityFullDto
{
    [Required] public Guid id { get; set; }
    [Required] public DateTime createTime { get; set; }
    [Required] [MinLength(1)] public string name { get; set; }
    public string description { get; set; }
    [Required] public bool isClosed { get; set; }
    [Required] public Int32 subscribersCount { get; set; }
    [Required] public List<UserDto> administrators { get; set; }
}