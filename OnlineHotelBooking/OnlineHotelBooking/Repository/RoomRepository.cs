using Microsoft.EntityFrameworkCore;
using OnlineHotelBooking.Data;
using OnlineHotelBooking.Interfaces;
using OnlineHotelBooking.Models;

namespace OnlineHotelBooking.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly ApplicationDbContext _context;
        public RoomRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Room room)
        {
            _context.Add(room);
            return Save();
        }

        public bool Delete(Room room)
        {
            _context.Remove(room);
            return Save();
        }

        public async Task<IEnumerable<Room>> GetAll()
        {
            return await _context.Rooms.ToListAsync();
        }

        public async Task<Room> GetByIdAsync(int id)
        {
            return await _context.Rooms.Include(i => i.Rents).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Room>> GetFreeRooms()
        {
            return await _context.Rooms.Where(r => r.Rents == null || !r.Rents.Any()).ToListAsync();
        }

        public async Task<IEnumerable<Room>> GetNotFreeRooms()
        {
            return await _context.Rooms.Where(r => r.Rents != null && r.Rents.Any()).ToListAsync();
        }


        public async Task<IEnumerable<Room>> GetRoomByUserId(string id)
        {
            return await _context.Rooms
                .Where(r => r.Rents.Any(rent => rent.UserId == id))
                .ToListAsync();
        }


        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Room room)
        {
            _context.Update(room);
            return Save();
        }
    }
}
