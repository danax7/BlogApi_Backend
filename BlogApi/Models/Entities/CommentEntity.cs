using System.ComponentModel.DataAnnotations;

namespace BlogApi.Entity;

public class CommentEntity
{
    [Required] public string id { get; set; }
    [Required] public string createTime { get; set; }
    [Required] [MinLength(1)] public string content { get; set; }
    public DateTime modifiedDate { get; set; }
    public DateTime deleteDate { get; set; }
    [Required] public Guid authorId { get; set; }
    [Required] [MinLength(1)] public string author { get; set; }
    public Int32 subComments { get; set; }
    
    [Required]
    public Guid UserId { get; set; }
    public UserEntity User { get; set; }
    
    [Required]
    public Guid PostId { get; set; }
    public PostEntity Post { get; set; }
    
    public string? ParentCommentId { get; set; }
    public CommentEntity ParentComment { get; set; }
}