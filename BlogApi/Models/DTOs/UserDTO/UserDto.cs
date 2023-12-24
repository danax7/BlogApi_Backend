using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BlogApi.Entity;
using BlogApi.Entity.Enums;


namespace BlogApi.DTO;

public class UserDto
{
    public Guid id { get; set; }
    public DateTime createTime { get; set; }
    [MinLength(1)] [Required] public String fullName { get; set; }
    [MinLength(6)] [Required] public String password { get; set; }
    [MinLength(1)] [Required] public String email { get; set; }
    public DateTime birthDate { get; set; }

    [Required]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Gender gender { get; set; }

    public String phoneNumber { get; set; }

    public UserDto()
    {
    }

    public UserDto(UserEntity userEntity)
    {
        id = userEntity.Id;
        createTime = userEntity.CreateTime;
        fullName = userEntity.FullName;
        password = userEntity.Password;
        email = userEntity.Email;
        birthDate = userEntity.BirthDate;
        gender = userEntity.Gender;
        phoneNumber = userEntity.PhoneNumber;
    }
}