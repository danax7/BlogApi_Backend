using System.ComponentModel.DataAnnotations;

namespace BlogApi.Entity;

public class TokenEntity
{
    public Guid id { get; set; }
    [Required] public String token { get; set; }
    [Required] public DateTime expiryDate { get; set; }
}