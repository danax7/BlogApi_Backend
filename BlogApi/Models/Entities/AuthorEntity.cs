using BlogApi.DTO;
using BlogApi.Entity.Enums;

namespace BlogApi.Entity;

public class AuthorEntity
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    public List<PostEntity> Posts { get; set; }
    //TODO: Need to add UserEntity to AuthorEntity and add AuthorEntity to UserEntity

    public UserEntity User { get; set; }
    public DateTime Created { get; set; }

    //public UserEntity User { get; set; }


    public AuthorEntity()
    {
    }

    public AuthorEntity(UserDto user)
    {
        Id = Guid.NewGuid();
        FullName = user.fullName;
        BirthDate = user.birthDate;
        Gender = user.gender;
        Created = DateTime.Now;
    }
}