using Microsoft.EntityFrameworkCore;
using Mio.API.Context;
using Mio.Models.Packets;
using Mio.Models.Relations;
using Mio.Models.Sale;
using Mio.Models.Text;
using Mio.Models.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mio.API.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Commodity>> GetCommodities();
        Task<IEnumerable<Commodity>> GetCommodities(string search_string);
        Task<IEnumerable<Service>> GetServices();
        Task<IEnumerable<Service>> GetServices(string search_string);
        Task<IEnumerable<Service>> GetFreeServices(int[] timeslotIds);
        Task<IEnumerable<Service>> GetFreeServices(ServicePacket servicePacket);
        Task<IEnumerable<Product>> GetProducts(string userId);
        Task<IEnumerable<Commodity>> GetRentals(string search_string);
        Task<IEnumerable<ProductRentals>> GetRentalsForUser(string userId);
        Task<IEnumerable<ProductRentals>> GetRentalsOwnedByUser(string userId);
        Task<IEnumerable<ProductRentals>> GetRentalsExpiringSoon(string userId);
        Task<IEnumerable<ServiceTimeslot>> GetUpcomingAppointmentsUser(string userId);
        Task<IEnumerable<ServiceTimeslot>> GetUpcomingAppointmentsServer(string userId);
        Task<IEnumerable<ServiceTimeslot>> GetAppointmentsUser(string customerID);
        Task<IEnumerable<ServiceTimeslot>> GetAppointmentsServer(string serverID);
        Task<IEnumerable<ProductHistory>> GetProductHistory(string userId);


        Task<Commodity> GetCommodity(int id);
        Task<Service> GetService(int id);
        Task<ServiceTimeslot> GetAppointment(int id);
        Task<ProductRentals> GetRental(int id);
        Task<Commodity> AddCommodity(Commodity commodity);
        Task<Service> AddService(Service service);
        Task<ServiceTimeslot> AddAppointment(ServicePacket servicePacket);
        Task<ProductHistory> AddProductHistory(ProductHistory productHistory);
        Task<ProductType> AddProductType(ProductType productType);
        Task<ProductRentals> AddProductRentals(ProductRentals productRentals);
        Task<ServiceTimeslot> AddServiceTimeslot(ServiceTimeslot serviceTimeslot);

        Task<Commodity> DeleteCommodity(int id);
        Task<Service> DeleteService(int id);
        Task<Product> DeleteProductRating(int reviewId);
        Task<ProductHistory> DeleteProductHistory(int id);
        Task<ProductType> DeleteProductType(ProductType productType);
        Task<ProductRentals> DeleteProductRentals(int id);
        Task<ServiceTimeslot> DeleteServiceTimeslot(int id);

        Task<Commodity> UpdateCommodity(Commodity commodity);
        Task<Product> UpdateProductRating(int  reviewId);
        Task<Service> UpdateService(Service service);
        Task<ProductRentals> UpdateProductRentals(ProductRentals productRentals);
        Task<ServiceTimeslot> UpdateServiceTimeslot(ServiceTimeslot serviceTimeslot);

    }

    public class ProductRepository : IProductRepository
    {
        private readonly MioContext _context;

        public ProductRepository(MioContext mioContext)
        {
            this._context = mioContext;
        }

        public async Task<ServiceTimeslot> AddAppointment(ServicePacket servicePacket)
        {
            var appointment = await _context.ServiceTimeslots.FirstOrDefaultAsync(x => x.ID == servicePacket.ID && x.CustomerID == null);
            if(appointment != null)
            {
                appointment.CustomerID = servicePacket.UserID;
                _context.ServiceTimeslots.Attach(appointment);
                _context.Entry(appointment).Property(x => x.CustomerID).IsModified = true;
                await _context.SaveChangesAsync();
            }
            return appointment;
        }

        public async Task<Commodity> AddCommodity(Commodity commodity)
        {
            if(commodity.ProductTypeID == 0)
            {
                await this.AddProductType(commodity.ProductType);
            }
            else
            {
                var productType = await _context.ProductTypes.FirstOrDefaultAsync(x => x.ID == commodity.ProductTypeID);
                productType.NumberProducts += 1;
                _context.ProductTypes.Attach(productType);
                _context.Entry(productType).Property(x => x.NumberProducts).IsModified = true;
            }
            var result = await _context.Commodities.AddAsync(commodity);
            if(result != null && commodity.ProductOption != null)
            {
                await _context.ProductOptions.AddAsync(commodity.ProductOption);
            }
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<ProductHistory> AddProductHistory(ProductHistory productHistory)
        {
            var result = await _context.ProductHistories.AddAsync(productHistory);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<ProductRentals> AddProductRentals(ProductRentals productRentals)
        {
            var result = await _context.ProductRentals.AddAsync(productRentals);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<ProductType> AddProductType(ProductType productType)
        {
            productType.NumberProducts = 1;
            var result = await _context.ProductTypes.AddAsync(productType);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Service> AddService(Service service)
        {
            if (service.ProductTypeID == 0)
            {
                await this.AddProductType(service.ProductType);
            }
            else
            {
                var productType = await _context.ProductTypes.FirstOrDefaultAsync(x => x.ID == service.ProductTypeID);
                productType.NumberProducts += 1;
                _context.ProductTypes.Attach(productType);
                _context.Entry(productType).Property(x => x.NumberProducts).IsModified = true;
            }
            var result = await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<ServiceTimeslot> AddServiceTimeslot(ServiceTimeslot serviceTimeslot)
        {
            if(await _context.ServiceTimeslots.FirstOrDefaultAsync(x => x.CustomerID == serviceTimeslot.CustomerID && x.ServerID == serviceTimeslot.ServerID) == null)
            {
                return null;
            }
            var result = await _context.ServiceTimeslots.AddAsync(serviceTimeslot);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Commodity> DeleteCommodity(int id)
        {
            var result = await _context.Commodities.FirstOrDefaultAsync(e => e.ID == id);
            if (result != null)
            {
                var reviewIDs = await _context.Reviews.Where(x => x.ProductID == result.ID).Select(x => x.ID).ToListAsync();
                //foreach(var reviewId in reviewIDs)
                //{
                //    await textContentRepository.DeleteReview(reviewId);
                //}

                var productType = await _context.ProductTypes.FirstOrDefaultAsync(x => x.ID == result.ProductTypeID);
                if(productType.NumberProducts == 1) { 
                    await DeleteProductType(productType);
                }
                else
                {
                    productType.NumberProducts -= 1;
                    _context.ProductTypes.Attach(productType);
                    _context.Entry(productType).Property(x => x.NumberProducts).IsModified = true;
                }

                _context.Commodities.Remove(result);
                await _context.SaveChangesAsync();
            }
            return result;
        }

        public async Task<ProductHistory> DeleteProductHistory(int id)
        {
            var result = await _context.ProductHistories.FirstOrDefaultAsync(e => e.ID == id);
            if (result != null)
            {
                _context.ProductHistories.Remove(result);
                await _context.SaveChangesAsync();
            }
            return result;
        }

        public async Task<Product> DeleteProductRating(int reviewId)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(e => e.ID == reviewId);
            if(review != null) {
                var result = await _context.Products.FirstOrDefaultAsync(e => e.ID == review.ProductID);
                if (result != null)
                {
                    result.Rating = (result.Rating * result.NumberReviews - review.Rating) / (result.NumberReviews - 1);
                    result.NumberReviews -= 1;
                    _context.Products.Attach(result);
                    _context.Entry(result).Property(x => x.NumberReviews).IsModified = true;
                    _context.Entry(result).Property(x => x.Rating).IsModified = true;
                    await _context.SaveChangesAsync();
                }
                return result;
            }
             return null;
        }

        public async Task<ProductRentals> DeleteProductRentals(int id)
        {
            var result = await _context.ProductRentals.FirstOrDefaultAsync(e => e.ID == id);
            if (result != null)
            {
                _context.ProductRentals.Remove(result);
                await _context.SaveChangesAsync();
            }
            return result;
        }

        public async Task<ProductType> DeleteProductType(ProductType productType)
        {
            var result =  _context.ProductTypes.Remove(productType);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Service> DeleteService(int id)
        {
            var result = await _context.Services.FirstOrDefaultAsync(e => e.ID == id);
            if (result != null)
            {
                var reviewIDs = await _context.Reviews.Where(x => x.ProductID == result.ID).Select(x => x.ID).ToListAsync();
                //foreach (var reviewId in reviewIDs)
                //{
                //    await textContentRepository.DeleteReview(reviewId);
                //}

                var productType = await _context.ProductTypes.FirstOrDefaultAsync(x => x.ID == result.ProductTypeID);
                if (productType.NumberProducts == 1)
                {
                    await DeleteProductType(productType);
                }
                else
                {
                    productType.NumberProducts -= 1;
                    _context.ProductTypes.Attach(productType);
                    _context.Entry(productType).Property(x => x.NumberProducts).IsModified = true;
                }

                _context.Services.Remove(result);
                await _context.SaveChangesAsync();
            }
            return result;
        }

        public async Task<ServiceTimeslot> DeleteServiceTimeslot(int id)
        {
            var result = await _context.ServiceTimeslots.FirstOrDefaultAsync(e => e.ID == id);
            if (result != null)
            {
                _context.ServiceTimeslots.Remove(result);
                await _context.SaveChangesAsync();
            }
            return result;
        }

        public async Task<ServiceTimeslot> GetAppointment(int id)
        {
            return await _context.ServiceTimeslots.Include(x => x.Service).FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<IEnumerable<Commodity>> GetCommodities()
        {
            return await _context.Commodities.ToListAsync();
        }

        public async Task<IEnumerable<Commodity>> GetCommodities(string search_string)
        {
            var productType = await _context.ProductTypes.FirstOrDefaultAsync(x => x.Name.Contains(search_string));
            if(productType != null) { return await _context.Commodities.Where(x => x.ProductTypeID == productType.ID).ToListAsync(); }
            return await _context.Commodities.Where(x => x.Name.Contains(search_string) || x.Description.Contains(search_string)).ToListAsync();
        }

        public async Task<Commodity> GetCommodity(int id)
        {
            return await _context.Commodities.FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<IEnumerable<ProductHistory>> GetProductHistory(string userId)
        {
            return await _context.ProductHistories.Where(x => x.UserID == userId).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts(string userId)
        {
            return await _context.Products.Where(x => x.UserID == userId).ToListAsync();
        }

        public async Task<ProductRentals> GetRental(int id)
        {
            return await _context.ProductRentals.FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<IEnumerable<Commodity>> GetRentals(string search_string)
        {
            var productType = await _context.ProductTypes.FirstOrDefaultAsync(x => x.Name.Contains(search_string));
            if (productType != null) { return await _context.Commodities.Where(x => x.RentAvailable && x.ProductTypeID == productType.ID).ToListAsync(); }
            return await _context.Commodities.Where(x => x.RentAvailable &&  (x.Name.Contains(search_string) || x.Description.Contains(search_string))).ToListAsync();
        }

        public async Task<IEnumerable<ProductRentals>> GetRentalsExpiringSoon(string userId)
        {
            return await _context.ProductRentals.Where(x => x.UserID == userId && x.ExpirationDate < DateTime.Now.AddDays(7)).ToListAsync();
        }

        public async Task<IEnumerable<ProductRentals>> GetRentalsForUser(string userId)
        {
            return await _context.ProductRentals.Where(x => x.UserID == userId).ToListAsync();
        }

        public async Task<IEnumerable<ProductRentals>> GetRentalsOwnedByUser(string userId)
        {
            return await _context.ProductRentals.Include(x => x.Product).Where(x => x.Product.UserID == userId).ToListAsync();
        }

        public async Task<Service> GetService(int id)
        {
            return await _context.Services.FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<IEnumerable<Service>> GetServices()
        {
            return await _context.Services.ToListAsync();
        }

        public async Task<IEnumerable<Service>> GetServices(string search_string)
        {
            var productType = await _context.ProductTypes.FirstOrDefaultAsync(x => x.Name.Contains(search_string));
            if (productType != null) { return await _context.Services.Where(x => x.ProductTypeID == productType.ID).ToListAsync(); }
            return await _context.Services.Where(x => x.Name.Contains(search_string) || x.Description.Contains(search_string)).ToListAsync();
        }

        public async Task<IEnumerable<Service>> GetFreeServices(int[] timeslotIds)
        {
            HashSet<int> hashset = new HashSet<int>(timeslotIds);
            return await _context.ServiceTimeslots.Where(x => hashset.Contains(x.TimeslotID) && x.CustomerID == null).Include(x => x.Service).Select(x => x.Service).ToListAsync();
        }

        public async Task<IEnumerable<Service>> GetFreeServices(ServicePacket servicePacket)
        {
            HashSet<int> hashset = new HashSet<int>(servicePacket.Ids);
            if (servicePacket.UserID == null)
            {
                return await _context.ServiceTimeslots.Include(x => x.Service).Where(x => hashset.Contains(x.TimeslotID) && x.CustomerID == null && 
                    (x.Service.Name.Contains(servicePacket.SearchString) || x.Service.Description.Contains(servicePacket.SearchString))).Select(x => x.Service).ToListAsync();
            }
            else
            {
                return await _context.ServiceTimeslots.Include(x => x.Service).Where(x => hashset.Contains(x.TimeslotID) && x.CustomerID == null && x.ServerID == servicePacket.UserID &&
                    (x.Service.Name.Contains(servicePacket.SearchString) || x.Service.Description.Contains(servicePacket.SearchString))).Select(x => x.Service).ToListAsync();
            }
        }
        public async Task<IEnumerable<ServiceTimeslot>>  GetAppointmentsUser(string customerID)
        {
            return await _context.ServiceTimeslots.Where(x => x.CustomerID == customerID).ToListAsync();
        }

        public async Task<IEnumerable<ServiceTimeslot>> GetAppointmentsServer(string serverID)
        {
            return await _context.ServiceTimeslots.Where(x => x.ServerID == serverID).ToListAsync();
        }


        public async Task<IEnumerable<ServiceTimeslot>> GetUpcomingAppointmentsServer(string userId)
        {
            return await _context.ServiceTimeslots.Where(x => x.ServerID == userId && x.Date <= DateTime.Now.AddDays(7)).ToListAsync();
        }

        public async Task<IEnumerable<ServiceTimeslot>> GetUpcomingAppointmentsUser(string userId)
        {
            return await _context.ServiceTimeslots.Where(x => x.CustomerID == userId && x.Date <= DateTime.Now.AddDays(7)).ToListAsync();
        }

        public async Task<Commodity> UpdateCommodity(Commodity commodity)
        {
            var entity = _context.Commodities.Find(commodity.ID);

            if (entity == null)
            {
                return null;
            }

            _context.Entry(entity).CurrentValues.SetValues(commodity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Product> UpdateProductRating(int reviewId)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(e => e.ID == reviewId);
            if (review != null)
            {
                var result = await _context.Products.FirstOrDefaultAsync(e => e.ID == review.ProductID);
                if (result != null)
                {
                    result.Rating = (result.Rating * result.NumberReviews + review.Rating) / (result.NumberReviews + 1);
                    result.NumberReviews += 1;
                    _context.Products.Attach(result);
                    _context.Entry(result).Property(x => x.NumberReviews).IsModified = true;
                    _context.Entry(result).Property(x => x.Rating).IsModified = true;
                    await _context.SaveChangesAsync();
                }
                return result;
            }
            return null;
        }

        public async Task<ProductRentals> UpdateProductRentals(ProductRentals productRentals)
        {
            var entity = _context.ProductRentals.Find(productRentals.ID);

            if (entity == null)
            {
                return null;
            }

            _context.Entry(entity).CurrentValues.SetValues(productRentals);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Service> UpdateService(Service service)
        {
            var entity = _context.Services.Find(service.ID);

            if (entity == null)
            {
                return null;
            }

            _context.Entry(entity).CurrentValues.SetValues(service);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<ServiceTimeslot> UpdateServiceTimeslot(ServiceTimeslot serviceTimeslot)
        {
            var entity = _context.ServiceTimeslots.Find(serviceTimeslot.ID);

            if (entity == null)
            {
                return null;
            }

            _context.Entry(entity).CurrentValues.SetValues(serviceTimeslot);
            await _context.SaveChangesAsync();
            return entity;
        }

    }
}
