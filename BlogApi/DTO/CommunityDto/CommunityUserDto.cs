using System.ComponentModel.DataAnnotations;
using BlogApi.Entity.Enums;

namespace BlogApi.DTO.CommunityDto;

public class CommunityUserDto
{
    [Required] public Guid userId { get; set; }
    [Required] public Guid communityId { get; set; }
    [Required] public CommunityRole role { get; set; }
}