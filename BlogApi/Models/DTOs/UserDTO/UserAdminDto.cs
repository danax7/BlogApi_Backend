using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BlogApi.Entity;
using BlogApi.Entity.Enums;

namespace BlogApi.DTO;

public class UserAdminDto
{
    public Guid id { get; set; }
    public DateTime createTime { get; set; }
    [Required] public String fullName { get; set; }
    [Required] public String email { get; set; }
    public DateTime birthDate { get; set; }

    [Required]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Gender gender { get; set; }

    public String phoneNumber { get; set; }

    public UserAdminDto()
    {
    }

    public UserAdminDto(UserEntity userEntity)
    {
        id = userEntity.Id;
        createTime = userEntity.CreateTime;
        fullName = userEntity.FullName;
        email = userEntity.Email;
        birthDate = userEntity.BirthDate;
        gender = userEntity.Gender;
        phoneNumber = userEntity.PhoneNumber;
    }
}