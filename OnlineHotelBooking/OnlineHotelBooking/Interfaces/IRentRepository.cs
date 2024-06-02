using OnlineHotelBooking.Models;

namespace OnlineHotelBooking.Interfaces
{
    public interface IRentRepository
    {
        Task<IEnumerable<Rent>> GetAll();
        Task<IEnumerable<Rent>> GetAllCurrent();

        Task<IEnumerable<Rent>> GetAllByUserAsync(string id);
        Task<Rent> GetRentByIdAsync(int id);
        Task<IEnumerable<Rent>> GetRentByUserId(string id);
        Task<IEnumerable<Rent>> GetRentByRoomId(int id);
        Task<Rent> GetActiveRentByUserAsync(string userId);

        Task<IEnumerable<Rent>> GetActiveRents();


        bool Add(Rent rent);
        bool Update(Rent rent);
        bool Delete(Rent rent);
        bool Save();
    }
}
