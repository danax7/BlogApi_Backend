using System.ComponentModel.DataAnnotations;
using BlogApi.Entity;

namespace BlogApi.DTO.PostDTO
{
    public class CreatePostDto
    {
        [Required] [MinLength(5)] public string title { get; set; }
        [Required] [MinLength(5)] public string description { get; set; }
        [Required] public int readTime { get; set; }
        public Guid? communityId { get; set; }
        public string? communityName { get; set; }
        public string image { get; set; }
        public string addressId { get; set; }

        // [Required] [MaxLength(1)] public List<string> tags { get; set; }
        [Required] [MinLength(1)] public List<string> tags { get; set; }
    }
}