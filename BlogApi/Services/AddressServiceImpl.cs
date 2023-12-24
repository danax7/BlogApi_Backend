using BlogApi.DTO.AddressDTO;
using BlogApi.Entity;
using BlogApi.Exception;
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
        var address = await _addressRepository.GetAddressByGuid(objectGuid);
        if (address == null)
        {
            throw new NotFoundException("Address not found");
        }

        var path = await _addressRepository.GetAddressChain(address.objectGuid);

        return path.Select(x => new SearchAddressDto(x)).ToList();
    }

    public async Task<List<SearchAddressDto>> Search(Int32? parentObjectId, String? query)
    {
        var addresses = await _addressRepository.SearchAddressesWithParentId(parentObjectId, query);
        var result = addresses.Select(x => new SearchAddressDto(x)).ToList();

        return result;
    }
}