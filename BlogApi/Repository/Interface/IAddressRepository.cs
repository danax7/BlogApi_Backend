using BlogApi.Entity;

namespace BlogApi.Repository.Interface;

public interface IAddressRepository
{
    Task<AddressEntity?> GetAddressByGuid(Guid objectGuid);
    Task<List<AddressEntity>> GetAddressChain(Guid objectId);
    Task<List<AddressEntity>> SearchAddressesWithParentId(Int64? parentId, string query);
    Task<AddressEntity?> GetAddressById(Int32 objectId);
    // Task<Boolean> CheckAddress(Guid id);
    
}