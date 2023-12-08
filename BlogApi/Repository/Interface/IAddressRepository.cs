using BlogApi.Entity;

namespace BlogApi.Repository.Interface;

public interface IAddressRepository
{
    Task<List<AddressEntity>> GetAddressChainByParentId(Int32? parentId);
    Task<AddressEntity?> GetAddressByGuid(Guid objectGuid);
    Task<AddressEntity?> GetAddressById(Int32 objectId);
    // Task<Boolean> CheckAddress(Guid id);
    
}