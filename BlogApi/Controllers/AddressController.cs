using BlogApi.DTO.AddressDTO;
using BlogApi.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers;

[ApiController]
[Route("api/address")]
public class AddressController : ControllerBase
{
    private readonly IAddressService _addressService;

    public AddressController(IAddressService addressService)
    {
        _addressService = addressService;
    }

    [HttpGet("search")]
    public async Task<List<SearchAddressDto>> Search([FromQuery] Int32? parentObjectId, [FromQuery] string? query)
    {
        return await _addressService.Search(parentObjectId, query);
    }
    
    [HttpGet("chain")]
    public async Task<List<SearchAddressDto>> GetAddressChain([FromQuery] Guid objectGuid)
    {
        return await _addressService.GetAddressChain(objectGuid);
    }
    
}