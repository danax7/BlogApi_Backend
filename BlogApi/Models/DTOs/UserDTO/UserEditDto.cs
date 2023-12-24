using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BlogApi.Entity.Enums;
using BlogApi.Validate;

namespace BlogApi.DTO;

public class UserEditDto
{
    [MinLength(1)] [Required] public String fullName { get; set; }
    [DateValidation(ErrorMessage = "Date must be less than today")]
    public DateTime birthDate { get; set; }
    
    [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$")]
    [MinLength(1)]
    [Required]
    public String email { get; set; }
    
    [Required]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Gender gender { get; set; }

    [RegularExpression(@"^\+7 \(\d{3}\) \d{3}-\d{2}-\d{2}$")]
    public String phoneNumber { get; set; }
}