using Microsoft.EntityFrameworkCore;
using Mio.API.Context;
using Mio.Models.Packets;
using Mio.Models.Relations;
using Mio.Models.Time;
using Mio.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mio.API.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<IEnumerable<User>> SearchByName(string name);
        Task<User> GetUser(string userId);
        Task<User> AddUser(User user);
        Task<bool> SetUsername(UserPacket userPacket);
        Task<User> UpdateUser(User user);
        Task<User> DeleteUser(string userId);
        Task<bool> UsernameExists(string username);
        Task<IEnumerable<Timeslot>> GetTimeslots(string userID);
        Task<IEnumerable<Timeslot>> GetTimeslotsForWeek(UserPacket userPacket);
        Task<Timeslot> AddTimeslot(UserPacket userPacket);
        Task<UserTimeslots> UpdateTimeslot(UserPacket userPacket);
        Task<UserTimeslots> DeleteTimeslot(UserPacket userPacket);
    }

    public class UserRepository : IUserRepository
    {
        private readonly MioContext _context;

        public UserRepository(MioContext mioContext)
        {
            this._context = mioContext;
        }

        public async Task<Timeslot> AddTimeslot(UserPacket userPacket)
        {
            var timeslot = await _context.Timeslots.FirstOrDefaultAsync(x => x.StartTime == userPacket.Timeslot.StartTime && x.Day == userPacket.Timeslot.Day);
            if(timeslot == null)
            {
               timeslot =  (await _context.Timeslots.AddAsync(userPacket.Timeslot)).Entity;
            }

            await _context.UserTimeslots.AddAsync(new UserTimeslots { UserID = userPacket.ID, EndDate = userPacket.DateTime, Repitition = userPacket.FieldValue, Available = true, TimeslotID = userPacket.Timeslot.ID });
            await _context.SaveChangesAsync();
            return timeslot;
        }

        public async Task<User> AddUser(User user)
        {
            bool exists = _context.Users.Any(u => u.ID == user.ID || user.UserName == u.UserName);
            if (!exists)
            {
                user.Rating = 0;

                var result = await _context.Users.AddAsync(user);

                await _context.SaveChangesAsync();

                return result.Entity;
            }
            return null;
        }

        public async Task<UserTimeslots> DeleteTimeslot(UserPacket userPacket)
        {
            var result = await _context.UserTimeslots.FirstOrDefaultAsync(e => e.UserID == userPacket.ID && e.TimeslotID == userPacket.Timeslot.ID);
            if (result != null)
            {
                _context.UserTimeslots.Remove(result);
                await _context.SaveChangesAsync();
            }
            return result;
        }

        public async Task<User> DeleteUser(string userId)
        {
            var result = await _context.Users.FirstOrDefaultAsync(e => e.ID == userId);
            if (result != null)
            {
                _context.Users.Remove(result);
                await _context.SaveChangesAsync();
            }
            return result;
        }

        public async Task<IEnumerable<Timeslot>> GetTimeslots(string userID)
        {
            return await _context.UserTimeslots.Where(x => x.UserID == userID).Include(x => x.Timeslot).Select(x => x.Timeslot).ToListAsync();
        }

        private bool DateInWeek(DateTime week, DateTime date)
        {
            return ((week.DayOfYear - date.DayOfYear) + date.DayOfWeek <= DayOfWeek.Saturday);
        }

        private DateTime SmallerDate(DateTime date1, DateTime date2)
        {
            return (date1 < date2) ? date1 : date2;
        }

        public async Task<IEnumerable<Timeslot>> GetTimeslotsForWeek(UserPacket userPacket)
        {
            var timeslots = await _context.UserTimeslots.Where(x => x.UserID == userPacket.ID).Include(x=>x.Timeslot).ToListAsync();
            IList<Timeslot> final_timeslots = new List<Timeslot>();

            var furthest_date = new DateTime();
            UserPacket packet = new UserPacket { ID = userPacket.ID };

            foreach (var timeslot in timeslots)
            {
                if(timeslot.EndDate < DateTime.Now)
                {
                    packet.Timeslot = timeslot.Timeslot;
                    await this.DeleteTimeslot(packet);
                    continue;
                }

                switch (timeslot.Repitition)
                {
                    case "daily":
                        furthest_date = timeslot.EarliestDate.AddDays(SmallerDate(userPacket.DateTime, timeslot.EndDate).DayOfYear - timeslot.EarliestDate.DayOfYear);
                        break;
                    case "weekly":
                        furthest_date = timeslot.EarliestDate.AddDays(7*Math.Floor((SmallerDate(userPacket.DateTime, timeslot.EndDate).DayOfYear - timeslot.EarliestDate.DayOfYear)/7.0));
                        break;
                    case "monthly":
                        furthest_date = timeslot.EarliestDate.AddMonths(SmallerDate(userPacket.DateTime, timeslot.EndDate).Month - timeslot.EarliestDate.Month);
                        break;
                    case "yearly":
                        furthest_date = timeslot.EarliestDate.AddYears(SmallerDate(userPacket.DateTime, timeslot.EndDate).Year - timeslot.EarliestDate.Year);
                        break;
                }

                if (DateInWeek(userPacket.DateTime, furthest_date))
                    final_timeslots.Add(timeslot.Timeslot);
            }
            return final_timeslots;
        }

        public async Task<User> GetUser(string userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.ID == userId);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<IEnumerable<User>> SearchByName(string name)
        {
            return await _context.Users.Where(u => (u.FirstName + " " + u.LastName).Contains(name)).ToListAsync();
        }

        public async Task<bool> SetUsername(UserPacket userPacket)
        {
            if (!UsernameExists(userPacket.FieldValue).Result)
            {
                var user = new User() { ID = userPacket.ID, UserName = userPacket.FieldValue };

                _context.Users.Attach(user);
                _context.Entry(user).Property(x => x.UserName).IsModified = true;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<UserTimeslots> UpdateTimeslot(UserPacket userPacket)
        {
            var entity = await _context.UserTimeslots.FirstOrDefaultAsync(x => x.UserID == userPacket.ID && x.TimeslotID == userPacket.Timeslot.ID);

            if (entity == null)
            {
                return null;
            }

            entity.TimeslotID = userPacket.Timeslot.ID;

            _context.UserTimeslots.Attach(entity);
            _context.Entry(entity).Property(x => x.TimeslotID).IsModified = true;
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<User> UpdateUser(User user)
        {
            var entity = _context.Users.Find(user.ID);

            if (entity == null)
            {
                return null;
            }

            _context.Entry(entity).CurrentValues.SetValues(user);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> UsernameExists(string username)
        {
            return await _context.Users.AnyAsync(u => u.UserName == username);
        }
    }
}
