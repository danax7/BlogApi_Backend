using BlogApi.Entity.Enums;

namespace BlogApi.Entity;

public class AddressEntity
{
    public Int64 objectId { get; set; }
    public Guid objectGuid { get; set; }
    public Int64 parentId { get; set; }
    public String text { get; set; }
    public GarAddressLevel objectLevel { get; set; }
}