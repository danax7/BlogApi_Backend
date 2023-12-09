using BlogApi.Entity;

namespace BlogApi.Repository.Interface;

public interface IAddressRepository
{
    Task<AddressEntity?> GetAddressByGuid(Guid objectGuid);
    Task<List<AddressEntity>> GetAddressChain(Guid objectId);
    Task<List<AddressEntity>> GetAddressesWithParentId(Int64? parentId);
    Task<AddressEntity?> GetAddressById(Int32 objectId);
    // Task<Boolean> CheckAddress(Guid id);
    
}