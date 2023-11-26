using System.ComponentModel.DataAnnotations;
using BlogApi.Entity;
using BlogApi.Entity.Enums;

namespace BlogApi.DTO.AuthorDTO;

public class AuthorDto
{
    [Required] public Guid id { get; set; }
    [Required] [MinLength(1)] public string fullName { get; set; }
    public DateTime birthDate;
    [Required] public Gender gender { get; set; }
    public Int32 posts { get; set; }
    public Int32 likes { get; set; }
    public DateTime created { get; set; }

    public AuthorDto()
    {
    }

    public AuthorDto(AuthorEntity authorEntity)
    {
        id = authorEntity.Id;
        fullName = authorEntity.FullName;
        birthDate = authorEntity.BirthDate;
        posts = authorEntity.Posts.Count;
        likes = authorEntity.Posts.Sum(post => post.likesCount);
        created = authorEntity.Created;
    }
}