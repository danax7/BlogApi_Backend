using System.ComponentModel.DataAnnotations;
using BlogApi.DTO.CommentDTO;
using BlogApi.Entity;

namespace BlogApi.DTO.PostDTO;

public class PostFullDto
{
    [Required] public Guid id { get; set; }
    [Required] public DateTime createTime { get; set; }
    [Required] public string title { get; set; }
    [Required] public string description { get; set; }
    [Required] public int readingTime { get; set; }
    public string image { get; set; }
    [Required] public Guid authorId { get; set; }
    [Required] public string author { get; set; }
    public Guid? communityId { get; set; }
    public string? communityName { get; set; }
    public Guid? addressId { get; set; }
    [Required] public int likes { get; set; }
    [Required] public int commentsCount { get; set; }
    public List<TagDto.TagDto> tags { get; set; }
    [Required] public List<CommentDto> comments { get; set; }

    public PostFullDto(PostEntity postEntity, List<CommentDto?> comments)
    {
        id = postEntity.id;
        createTime = postEntity.createTime;
        title = postEntity.title;
        description = postEntity.description;
        readingTime = postEntity.readingTime;
        image = postEntity.image;
        authorId = postEntity.authorId;
        author = postEntity.Author.FullName;
        communityId = postEntity.communityId ?? Guid.Empty;
        communityName = postEntity.communityName ?? "";
        addressId = postEntity.addressId;
        likes = postEntity.likesCount;
        commentsCount = postEntity.commentsCount;
        tags = postEntity.tags.ConvertAll(tagEntity => new TagDto.TagDto(tagEntity));
        this.comments = comments;
    }

    public PostFullDto()
    {
    }
}