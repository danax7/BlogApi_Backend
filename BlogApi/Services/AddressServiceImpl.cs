using BlogApi.DTO.AddressDTO;
using BlogApi.Entity;
using BlogApi.Repository.Interface;
using BlogApi.Services.Interface;

namespace BlogApi.Services;

public class AddressServiceImpl : IAddressService
{
    private readonly IAddressRepository _addressRepository;
    
    public AddressServiceImpl(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }
    
    public async Task<List<SearchAddressDto>> GetAddressChain(Guid objectGuid)
    {
        var addresses = new List<AddressEntity>();
        var address = await _addressRepository.GetAddressByGuid(objectGuid);
        while (address != null)
        {
            addresses.Add(address);
            address = await _addressRepository.GetAddressById(address.parentId);
        }

        addresses.Reverse();

        var result = addresses.Select(x => new SearchAddressDto(x)).ToList();
        return result;
    }
    
    public async Task<List<SearchAddressDto>> Search(Int32? parentObjectId, String query)
    {
        throw new NotImplementedException();
    }
    
}