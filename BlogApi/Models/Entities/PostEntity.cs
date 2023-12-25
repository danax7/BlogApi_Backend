using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BlogApi.DTO.CommentDTO;
using BlogApi.DTO.TagDto;

namespace BlogApi.Entity;

public class PostEntity
{
    [Required] public Guid id { get; set; }
    [Required] public DateTime createTime { get; set; }
    [Required] public string title { get; set; }
    [Required] public string description { get; set; }
    [Required] public int readingTime { get; set; }
    public AuthorEntity Author { get; set; }
    public string image { get; set; }
    [ForeignKey("Author")]
    [Required] public Guid authorId { get; set; }
    //[Required] public string author { get; set; }
    public Guid? communityId { get; set; }
    public string? communityName { get; set; }
    public Guid? addressId { get; set; }
    [Required] public int likesCount { get; set; }
    [Required] public int commentsCount { get; set; }

    //TODO: Check tags
    //public List<Guid> tags { get; set; }
    [Required] public List<TagEntity> tags { get; set; }

    // public List<UserEntity?> Users { get; set; }
    public List<LikeEntity> Likes { get; set; }

    public List<CommentEntity> Comments { get; set; }
    public CommunityEntity? Community { get; set; }

    public void AddLike()
    {
        likesCount++;
    }

    public void RemoveLike()
    {
        likesCount--;
    }

    public void IncrementCommentCount()
    {
        commentsCount++;
    }

    public void DecrementCommentCount()
    {
        commentsCount--;
    }
}