using Microsoft.EntityFrameworkCore;
using Mio.API.Context;
using Mio.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mio.API.Repositories
{
    public interface IAddressRepository
    {
        Task<IEnumerable<Address>> GetAddresses(string userId);
        Task<Address> AddAddress(Address address);
        Task<Address> UpdateAddress(Address address);
        Task<Address> DeleteAddress(int addressId);
        Task<Address> GetDefaultAddress(string userId);
    }

    public class AddressRepository : IAddressRepository
    {
        private readonly MioContext _context;

        public AddressRepository(MioContext mioContext)
        {
            this._context = mioContext;
        }

        public async Task<Address> AddAddress(Address address)
        {
            bool exists = _context.Addresses.Any(u => u.UserID == address.UserID && u.AddressLine1 == address.AddressLine1 && u.Zipcode == address.Zipcode);
            if (!exists)
            {
                var result = await _context.Addresses.AddAsync(address);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            return null;
        }

        public async Task<Address> DeleteAddress(int addressId)
        {
            var result = await _context.Addresses.FirstOrDefaultAsync(e => e.ID == addressId);
            if (result != null)
            {
                _context.Addresses.Remove(result);
                await _context.SaveChangesAsync();
            }
            return result;
        }

        public async Task<IEnumerable<Address>> GetAddresses(string userId)
        {
            return await _context.Addresses.Where(u => u.UserID == userId).ToListAsync();
        }

        public async Task<Address> GetDefaultAddress(string userId)
        {
            return await _context.Addresses.Where(u => u.UserID == userId).FirstOrDefaultAsync();
        }

        public async Task<Address> UpdateAddress(Address address)
        {
            var entity = _context.Addresses.Find(address.ID);

            _context.Entry(entity).CurrentValues.SetValues(address);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
