using System.ComponentModel.DataAnnotations;
using BlogApi.Entity;
using BlogApi.Entity.Enums;

namespace BlogApi.DTO.CommunityDto
{
    public class CommunityUserDto
    {
        [Required] public Guid userId { get; set; }
        [Required] public Guid communityId { get; set; }
        [Required] public CommunityRole role { get; set; }

        public CommunityUserDto()
        {
        }

        public CommunityUserDto(CommunityEntity community)
        {
            userId = community.UserCommunities.FirstOrDefault()?.UserId ?? Guid.Empty;
            communityId = community.id;
            role = community.UserCommunities.FirstOrDefault()?.Role ?? CommunityRole.Subscriber;
        }
    }
}