using BlogApi.Context;
using BlogApi.Entity;
using BlogApi.Entity.Enums;
using BlogApi.Helpers;
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

        private static AddressEntity MapAddress(AsAddrObj addressObj)
        {
            var text = $"{addressObj.Typename} {addressObj.Name}";
            var addressLevel = (GarAddressLevel)Enum.Parse(typeof(GarAddressLevel), addressObj.Level);

            return new AddressEntity(addressObj.Objectid, addressObj.Objectguid, text, addressLevel);
        }

        private static AddressEntity MapAddress(AsHouse addressObj)
        {
            var addtype1 = "";
            var addtype2 = "";
            if ( addressObj.Addtype1 != null)
            {
                 var addtype11 = (GarHouseAddtype)Enum.Parse(typeof(GarHouseAddtype), addressObj.Addtype1.ToString());
                 addtype1 = addtype11.GetDescription();
            }
            
            if (addressObj.Addtype2 != null)
            {
                 var addtype22 = (GarHouseAddtype)Enum.Parse(typeof(GarHouseAddtype), addressObj.Addtype2.ToString());
                 addtype2 = addtype22.GetDescription();
            }
            
            var text = $"{addressObj.Housenum} {addtype1} {addressObj.Addnum1} {addtype2} {addressObj.Addnum2}".TrimEnd();
            var addressLevel = (GarAddressLevel)Enum.Parse(typeof(GarBuildingLevel), "10");

            return new AddressEntity(addressObj.Objectid, addressObj.Objectguid, text, addressLevel);
        }

        public async Task<List<AddressEntity>> GetAddressChain(Guid objectGuid)
        {
            var address = await _context.AsAddrObjs.FirstOrDefaultAsync(x => x.Objectguid == objectGuid);
            var addressAsHouse = await _context.AsHouses.FirstOrDefaultAsync(x => x.Objectguid == objectGuid);

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
                        addresses.Add(MapAddress(addressTemp));
                    }

                    continue;
                }

                addresses.Add(MapAddress(addressEntity));
            }

            return addresses;
        }

        public async Task<AddressEntity?> GetAddressByGuid(Guid objectGuid)
        {
            var addressObj = await _context.AsAddrObjs.FirstOrDefaultAsync(x => x.Objectguid == objectGuid);
            var addressAsHouse = await _context.AsHouses.FirstOrDefaultAsync(x => x.Objectguid == objectGuid);
            if (addressAsHouse != null)
            {
                return MapAddress(addressAsHouse);
            }

            return addressObj != null ? MapAddress(addressObj) : null;
        }

        public async Task<AddressEntity?> GetAddressById(Int32 objectId)
        {
            var addressObj = await _context.AsAddrObjs.FirstOrDefaultAsync(x => x.Objectid == objectId);
            return addressObj != null ? MapAddress(addressObj) : null;
        }

        public async Task<List<AddressEntity>> SearchAddressesWithParentId(long? parentId, string? query)
        {
            if (parentId == null)
            {
                parentId = 0;
            }

            var addressIds = await _context.AsAdmHierarchies
                .Where(x => x.Parentobjid == parentId)
                .Select(x => x.Objectid)
                .ToListAsync();

            var addrObjsQuery = await _context.AsAddrObjs
                .Where(addrObj => addressIds.Contains(addrObj.Objectid))
                .Select(addrObj => MapAddress(addrObj))
                .ToListAsync();

            var housesQuery = await _context.AsHouses
                .Where(house => addressIds.Contains(house.Objectid))
                .Select(house => MapAddress(house))
                .ToListAsync();

            var result = addrObjsQuery
                .Concat(housesQuery)
                .Where(x => x.text.ToLower().Contains(query ?? ""))
                .Take(20)
                .ToList();

            return result;
        }

    }
}