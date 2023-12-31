using System.ComponentModel.DataAnnotations;
using BlogApi.Entity;

namespace BlogApi.DTO.CommentDTO;

public class CommentDto
{
    [Required] public Guid id { get; set; }
    [Required] public DateTime createTime { get; set; }
    [Required] [MinLength(1)] public string content { get; set; }
    public DateTime? modifiedDate { get; set; }
    public DateTime? deleteDate { get; set; }
    [Required] public Guid authorId { get; set; }
    [Required] [MinLength(1)] public string author { get; set; }
    public Int32 subComments { get; set; }

    public CommentDto(CommentEntity commentEntity)
    {
        id = commentEntity.id;
        createTime = commentEntity.createTime;
        content = commentEntity.content;
        authorId = commentEntity.authorId;
        author = commentEntity.author;
        subComments = commentEntity.subCommentsCount;
    }
}