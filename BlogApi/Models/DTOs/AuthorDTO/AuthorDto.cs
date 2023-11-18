using System.ComponentModel.DataAnnotations;
using BlogApi.Entity.Enums;

namespace BlogApi.DTO.AuthorDTO;

public class AuthorDto
{
    [Required] [MinLength(1)] public string fullName { get; set; }
    public DateTime birthDate;
    [Required] public Gender gender { get; set; }
    public Int32 posts { get; set; }
    public Int32 likes { get; set; }
    public DateTime created { get; set; }

}