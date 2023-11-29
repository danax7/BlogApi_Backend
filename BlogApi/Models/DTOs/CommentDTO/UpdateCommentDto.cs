using System.ComponentModel.DataAnnotations;

namespace BlogApi.DTO.CommentDTO;

public class UpdateCommentDto
{
    [Required] [MinLength(1)] public string content { get; set; }
    
}