using BlogApi.Context;
using BlogApi.Entity;
using BlogApi.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Services;

public class AddressRepositoryImpl : IAddressRepository
{
    private readonly AddressDbContext _context;
    
    public AddressRepositoryImpl(AddressDbContext context)
    {
        _context = context;
    }
    
    public Task<List<AddressEntity>> GetAddressChainByParentId(Int32? parentId)
    {
        throw new NotImplementedException();
    }
    
    public Task<AddressEntity?> GetAddressByGuid(Guid objectGuid)
    {
        throw new NotImplementedException();
    }
    
    public async Task<AddressEntity?> GetAddressById(Int32 objectId)
    {
        throw new NotImplementedException();
       // return await _context.AsAddrObjs.FirstOrDefaultAsync(x => x.Objectid == objectId);
    }
    
    
}