using BlogApi.Entity.Enums;

namespace BlogApi.Entity;

public class AddressEntity
{
    public Int32 objectId { get; set; }
    public Guid objectGuid { get; set; }
    public Int32 parentId { get; set; }
    public String text { get; set; }
    public GarAddressLevel objectLevel { get; set; }
}