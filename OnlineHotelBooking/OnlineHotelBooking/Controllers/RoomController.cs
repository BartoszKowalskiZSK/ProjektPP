using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineHotelBooking.Data;
using OnlineHotelBooking.Interfaces;
using OnlineHotelBooking.Models;

namespace OnlineHotelBooking.Controllers
{
    public class RoomController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IRoomRepository _roomRepository;
        public RoomController(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Room> rooms = await _roomRepository.GetFreeRooms();
            return View(rooms);
        }

        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Room room)
        {
            if(!ModelState.IsValid)
            {
                return View(room);
            }
            _roomRepository.Add(room);
            return RedirectToAction("Index");
        }
    }
}
