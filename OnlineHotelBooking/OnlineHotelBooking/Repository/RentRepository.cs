using Microsoft.EntityFrameworkCore;
using OnlineHotelBooking.Data;
using OnlineHotelBooking.Interfaces;
using OnlineHotelBooking.Models;

namespace OnlineHotelBooking.Repository
{
    public class RentRepository : IRentRepository
    {
        private readonly ApplicationDbContext _context;
        public RentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Rent rent)
        {
            _context.Add(rent);
            return Save();
        }

        public bool Delete(Rent rent)
        {
            _context.Remove(rent);
            return Save();
        }

        public async Task<IEnumerable<Rent>> GetActiveRents()
        {
            return await _context.Rents.Where(r => r.IsActive).ToListAsync();
        }

        public async Task<IEnumerable<Rent>> GetAll()
        {
            return await _context.Rents.ToListAsync();
        }

        public async Task<IEnumerable<Rent>> GetAllCurrent()
        {
            var today = DateTime.Today;
            return await _context.Rents.Where(r => r.From <= today && r.To >= today).ToListAsync();
        }

        public async Task<IEnumerable<Rent>> GetRentByRoomId(int id)
        {
            return await _context.Rents.Include(i => i).Where(r => r.RoomId == id).ToListAsync();
        }

        public async Task<IEnumerable<Rent>> GetRentByUserId(string id)
        {
            return await _context.Rents.Include(i => i).Where(r => r.UserId == id).ToListAsync();
        }

        public async Task<Rent> GetRentByIdAsync(int id)
        {
            return await _context.Rents.FindAsync(id);
        }

        public async Task<Rent> GetActiveRentByUserAsync(string userId)
        {
            return await _context.Rents.FirstOrDefaultAsync(r => r.UserId == userId && r.IsActive);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Rent rent)
        {
            _context.Update(rent);
            return Save();
        }

        public async Task<IEnumerable<Rent>> GetAllByUserAsync(string id)
        {
            // Użyj LINQ do zapytań, aby zwrócić wszystkie wynajmy danego użytkownika
            var rents = await _context.Rents
                .Where(r => r.UserId == id)
                .ToListAsync();

            return rents;
        }
    }
}
