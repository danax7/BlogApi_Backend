using BlogApi.Entity.Enums;

namespace BlogApi.Entity;

public class AddressEntity
{
    public Int64 objectId { get; set; }
    public Guid objectGuid { get; set; }
    public String? text { get; set; }
    public GarAddressLevel objectLevel { get; set; }
    
    public AddressEntity()
    {
    }
    
    public AddressEntity(Int64 objectId, Guid objectGuid, String? text, GarAddressLevel objectLevel)
    {
        this.objectId = objectId;
        this.objectGuid = objectGuid;
        this.text = text;
        this.objectLevel = objectLevel;
    }
}