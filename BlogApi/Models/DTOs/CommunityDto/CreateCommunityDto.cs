using System.ComponentModel.DataAnnotations;

namespace BlogApi.DTO.CommunityDto;

public class CreateCommunityDto
{
    [Required] [MinLength(1)] public string name { get; set; }
    [Required] public string description { get; set; }
    [Required] public bool isClosed { get; set; }
}