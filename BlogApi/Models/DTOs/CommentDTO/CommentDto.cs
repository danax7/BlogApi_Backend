using System.ComponentModel.DataAnnotations;

namespace BlogApi.DTO.CommentDTO;

public class CommentDto
{
    [Required] public string id { get; set; }
    [Required] public string createTime { get; set; }
    [Required] [MinLength(1)] public string content { get; set; }
    public DateTime modifiedDate { get; set; }
    public DateTime deleteDate { get; set; }
    [Required] public Guid authorId { get; set; }
    [Required] [MinLength(1)] public string author { get; set; }
    public Int32 subComments { get; set; }
    
}