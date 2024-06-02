using OnlineHotelBooking.Models;

namespace OnlineHotelBooking.Interfaces
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetAll();
        Task<Room> GetByIdAsync(int id);
        Task<IEnumerable<Room>> GetRoomByUserId(string id);

        Task<IEnumerable<Room>> GetFreeRooms();

        Task<IEnumerable<Room>> GetNotFreeRooms();
        //CRUD -------------------------------------------------------------------------------------------------------------------------------
        bool Add(Room room);
        bool Update(Room room);
        bool Delete(Room room);
        bool Save();
    }
}
