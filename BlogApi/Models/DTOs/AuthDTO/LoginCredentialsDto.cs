using System.ComponentModel.DataAnnotations;

namespace BlogApi.DTO.AuthDTO;

public class LoginCredentialsDto
{
    [Required] public String email { get; set; }
    [Required] public String password { get; set; }
}