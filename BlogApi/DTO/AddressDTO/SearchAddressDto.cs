using System.ComponentModel.DataAnnotations;
using BlogApi.Entity.Enums;

namespace BlogApi.DTO.AddressDTO;

public class SearchAddressDto
{
    [Required] public Int64 objectId { get; set; }
    [Required] public Guid objectGuid { get; set; }
    public String text { get; set; }
    [Required] public GarAddressLevel objectLevel { get; set; }
    public String objectLevelText { get; set; }

   
}