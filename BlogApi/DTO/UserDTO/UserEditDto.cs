using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BlogApi.Entity.Enums;

namespace BlogApi.DTO;

public class UserEditDto
{
    [MinLength(1)] [Required] public String fullName { get; set; }
    public DateTime birthDate { get; set; }

    [Required]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Gender gender { get; set; }

    [RegularExpression(@"^\+7 \(\d{3}\) \d{3}-\d{2}-\d{2}$")]
    public String phoneNumber { get; set; }
}