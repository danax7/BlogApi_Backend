using BlogApi.Context;
using BlogApi.Entity;
using BlogApi.Entity.Enums;
using BlogApi.Migrations;
using BlogApi.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Services
{
    public class AddressRepositoryImpl : IAddressRepository
    {
        private readonly AddressDbContext _context;

        public AddressRepositoryImpl(AddressDbContext context)
        {
            _context = context;
        }

        private async Task<AddressEntity> MapAddress(AsAddrObj addressObj)
        {
            var text = $"{addressObj.Typename} {addressObj.Name}";
            var addressLevel = (GarAddressLevel)Enum.Parse(typeof(GarAddressLevel), addressObj.Level);

            return new AddressEntity(addressObj.Objectid, addressObj.Objectguid, text, addressLevel);
        }

        private async Task<AddressEntity> MapAddress(AsHouse addressObj)
        {
            var text = $"{addressObj.Addnum1} {addressObj.Housenum}";
            var addressLevel = (GarAddressLevel)Enum.Parse(typeof(GarBuildingLevel), "10");

            return new AddressEntity(addressObj.Objectid, addressObj.Objectguid, text, addressLevel);
        }

        public async Task<List<AddressEntity>> GetAddressChain(Guid objectGuid)
        {
            var address = await _context.AsAddrObjs.FirstOrDefaultAsync(x => x.Objectguid == objectGuid);
            var addressAsHouse = await _context.AsHouses.FirstOrDefaultAsync(x => x.Objectguid == objectGuid);
            //Сделать еше обработку для домов с 4/2 и т.д.

            if (address == null && addressAsHouse == null)
            {
                return new List<AddressEntity>();
            }

            var path = new AsAdmHierarchy();
            if (address != null)
            {
                path = await _context.AsAdmHierarchies.FirstOrDefaultAsync(x => x.Objectid == address.Objectid);
            }

            if (addressAsHouse != null)
            {
                path = await _context.AsAdmHierarchies.FirstOrDefaultAsync(x => x.Objectid == addressAsHouse.Objectid);
            }

            var pathArray = path.Path.Split('.');
            var addresses = new List<AddressEntity>();

            foreach (var id in pathArray)
            {
                var addressEntity = await _context.AsAddrObjs.FirstOrDefaultAsync(x => x.Objectid == Int32.Parse(id));
                if (addressEntity == null)
                {
                    var addressTemp = await _context.AsHouses.FirstOrDefaultAsync(x => x.Objectid == Int32.Parse(id));
                    if (addressTemp != null)
                    {
                        addresses.Add(await MapAddress(addressTemp));
                    }
                    continue;
                }

                addresses.Add(await MapAddress(addressEntity));
            }

            return addresses;
        }

        public async Task<AddressEntity?> GetAddressByGuid(Guid objectGuid)
        {
            var addressObj = await _context.AsAddrObjs.FirstOrDefaultAsync(x => x.Objectguid == objectGuid);
            var addressAsHouse = await _context.AsHouses.FirstOrDefaultAsync(x => x.Objectguid == objectGuid);
            if (addressAsHouse != null)
            {
                return await MapAddress(addressAsHouse);
                //Сделать еше обработку для домов с 4/2 и т.д.
            }

            return addressObj != null ? await MapAddress(addressObj) : null;
        }

        public async Task<AddressEntity?> GetAddressById(Int32 objectId)
        {
            var addressObj = await _context.AsAddrObjs.FirstOrDefaultAsync(x => x.Objectid == objectId);
            return addressObj != null ? await MapAddress(addressObj) : null;
        }

        public async Task<List<AddressEntity>> GetAddressesWithParentId(long? parentId)
        {
            if (parentId == null)
            {
                parentId = 0;
            }
            var addresses = await _context.AsAdmHierarchies.Where(x => x.Parentobjid == parentId).ToListAsync();
            var result = new List<AddressEntity>();
            foreach (var address in addresses)
            {
                var addressObj = await _context.AsAddrObjs.FirstOrDefaultAsync(x => x.Objectid == address.Objectid);
                if (addressObj != null)
                {
                    result.Add(await MapAddress(addressObj));
                }
            }
            
            return result;
        }
    }
}