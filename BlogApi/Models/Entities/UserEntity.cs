using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BlogApi.DTO;
using BlogApi.Entity.Enums;

namespace BlogApi.Entity;

public class UserEntity
{
    public Guid Id { get; set; }

    // public Guid? AuthorId { get; set; }
    public DateTime CreateTime { get; set; }
    [MinLength(1)] [Required] public String FullName { get; set; }
    [MinLength(6)] [Required] public String Password { get; set; }
    [MinLength(1)] [Required] public String Email { get; set; }
    public DateTime BirthDate { get; set; }

    [Required]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Gender Gender { get; set; }

    public String PhoneNumber { get; set; }

    public List<UserCommunityEntity> UserCommunities { get; set; }

    public AuthorEntity? Author { get; set; }
    public List<CommentEntity> Comments { get; set; }
    // public List<PostEntity?> Posts { get; set; }


    public UserEntity(UserRegisterDto userRegisterDto)
    {
        Id = Guid.NewGuid();
        CreateTime = DateTime.Now;
        FullName = userRegisterDto.fullName;
        Password = userRegisterDto.password;
        Email = userRegisterDto.email;
        BirthDate = userRegisterDto.birthDate;
        Gender = userRegisterDto.gender;
        PhoneNumber = userRegisterDto.phoneNumber;
    }

    public UserEntity UpdateUser(UserEditDto userEditDto)
    {
        FullName = userEditDto.fullName;
        BirthDate = userEditDto.birthDate;
        Gender = userEditDto.gender;
        Email = userEditDto.email;
        PhoneNumber = userEditDto.phoneNumber;
        return this;
    }


    public UserEntity()
    {
    }
}