using BlogApi.Context;
using BlogApi.Entity;
using BlogApi.Entity.Enums;
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
    
    public async Task<List<AddressEntity>> getAddressChain(Guid objectGuid)
    {
        var address = await _context.AsAddrObjs.FirstOrDefaultAsync(x => x.Objectguid == objectGuid);
        var path = await _context.AsAdmHierarchies.FirstOrDefaultAsync(x => x.Objectid == address.Objectid);
        var pathArray = path.Path.Split('.');
        var addresses = new List<AddressEntity>();
        foreach (var id in pathArray)
        {
            var addressEntity = await _context.AsAddrObjs.FirstOrDefaultAsync(x => x.Objectid == Int32.Parse(id));
            var text = addressEntity.Typename + " " + addressEntity.Name;
            var addresslevel = (GarAddressLevel)Enum.Parse(typeof(GarAddressLevel), addressEntity.Level);
            addresses.Add(new AddressEntity(addressEntity.Objectid, addressEntity.Objectguid,text, addresslevel));
        }
        return addresses;
    }
    
    
    public  async Task<AddressEntity?> GetAddressByGuid(Guid objectGuid)
    {
        var address = await _context.AsAddrObjs.FirstOrDefaultAsync(x => x.Objectguid == objectGuid);
        if (address == null)
        {
            return null;
        }
        
        var text = address.Typename + " " + address.Name;
        var addresslevel = (GarAddressLevel)Enum.Parse(typeof(GarAddressLevel), address.Level);
        return new AddressEntity(address.Objectid, address.Objectguid,text, addresslevel);
    }
    
    public async Task<AddressEntity?> GetAddressById(Int32 objectId)
    {
        var address = await _context.AsAddrObjs.FirstOrDefaultAsync(x => x.Objectid == objectId);
        if (address == null)
        {
            return null;
        }

        var text = address.Typename + " " + address.Name;
        var addresslevel = (GarAddressLevel)Enum.Parse(typeof(GarAddressLevel), address.Level);
        return new AddressEntity(address.Objectid, address.Objectguid,text, addresslevel);
    }
    
}