using System.ComponentModel.DataAnnotations;
using BlogApi.Entity.Enums;

namespace BlogApi.DTO;

public class UserRegisterDto
{
    [MinLength(1)] [Required] public String fullName { get; set; }
    [MinLength(6)] [Required] public String password { get; set; }
    [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$")]
    [MinLength(1)] [Required] public String email { get; set; }

    //TODO: Add Validation for birthDate
    public DateTime birthDate { get; set; }
    [Required] public Gender gender { get; set; }

    [RegularExpression(@"^\+7 \(\d{3}\) \d{3}-\d{2}-\d{2}$")]
    public String phoneNumber { get; set; }
}