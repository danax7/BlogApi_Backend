using System.ComponentModel.DataAnnotations;
using BlogApi.Entity;
using BlogApi.Entity.Enums;
using BlogApi.Helpers;

namespace BlogApi.DTO.AddressDTO;

public class SearchAddressDto
{
    [Required] public Int64 objectId { get; set; }
    [Required] public Guid objectGuid { get; set; }
    public String text { get; set; }
    [Required] public string objectLevel { get; set; }
    public String objectLevelText { get; set; }

    public SearchAddressDto(AddressEntity addressEntity)
    {
        objectId = addressEntity.objectId;
        objectGuid = addressEntity.objectGuid;
        text = addressEntity.text;
        objectLevel = addressEntity.objectLevel.ToString();
        objectLevelText = addressEntity.objectLevel.GetDescription();
    }

    public SearchAddressDto()
    {
    }
}