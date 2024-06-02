using Microsoft.AspNetCore.Mvc;
using OnlineHotelBooking.Data;
using OnlineHotelBooking.Interfaces;
using OnlineHotelBooking.Models;
using OnlineHotelBooking.Repository;
using System.Security.Claims;

namespace OnlineHotelBooking.Controllers
{
    public class RentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IRentRepository _rentRepository;
        public RentController(IRentRepository rentRepository)
        {
            _rentRepository = rentRepository;
        }
        public async Task<IActionResult> Rents()
        {
            if(User.Identity.IsAuthenticated && User.IsInRole("user"))
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Pobierz id użytkownika
                IEnumerable<Rent> rents = await _rentRepository.GetAllByUserAsync(userId);
                return View(rents);
            }

            if (User.Identity.IsAuthenticated && User.IsInRole("worker"))
            {
                IEnumerable<Rent> rent = await _rentRepository.GetAll();
                return View(rent);
            }

            return RedirectToAction("Index", "Home");

        }

        public IActionResult Create(int? roomId, int? roomPrice)
        {
            ViewData["RoomId"] = roomId;
            ViewData["RoomPrice"] = roomPrice;
            return View();
        }

        public async Task<IActionResult> AllCurrentRents()
        {
            IEnumerable<Rent> rents = await _rentRepository.GetActiveRents();
            return View(rents);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Rent rent)
        {
            if (!ModelState.IsValid)
            {
                return View(rent);
            }

            // Sprawdź, czy użytkownik ma już aktywny wynajem
            var activeRent = await _rentRepository.GetActiveRentByUserAsync(rent.UserId);
            if (activeRent != null)
            {
                // Jeśli użytkownik ma już aktywny wynajem, możesz odpowiednio obsłużyć tę sytuację.
                ModelState.AddModelError(string.Empty, "Masz już aktywny wynajem.");
                return View(rent);
            }

            _rentRepository.Add(rent);
            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> Edit(int id)
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("worker"))
            {
                var rent = await _rentRepository.GetRentByIdAsync(id);
                return View("Edit", rent);
            }
            return View("Index", "Home");

        }

        [HttpPost]
        public async Task<IActionResult> Edit(Rent rent)
        {
            if (!ModelState.IsValid)
            {
                return View(rent);
            }
            _rentRepository.Update(rent);
            return RedirectToAction("Index", "Home");
        }

    }
}
