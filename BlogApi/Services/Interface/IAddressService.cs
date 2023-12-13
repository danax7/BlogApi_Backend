using BlogApi.DTO.AddressDTO;

namespace BlogApi.Services.Interface;

public interface IAddressService
{
    Task<List<SearchAddressDto>> Search(Int32? parentObjectId, String query);
    Task<List<SearchAddressDto>> GetAddressChain(Guid objectGuid);
}