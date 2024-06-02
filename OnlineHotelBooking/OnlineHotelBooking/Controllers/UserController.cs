using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OnlineHotelBooking.Data;
using OnlineHotelBooking.Models;

namespace OnlineHotelBooking.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserController(ApplicationDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;

        }
        public async Task<IActionResult> Users()
        {
            var users = await _userManager.Users.ToListAsync();

            // Pobierz role dostępne w systemie
            var roles = await _roleManager.Roles.ToListAsync();

            // Utwórz rozwijalną listę ról
            ViewBag.Roles = roles;

            // Przekaż listę użytkowników do widoku
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeRole(string userId, string roleId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound(); // Obsługa przypadku, gdy użytkownik nie istnieje
            }

            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return NotFound(); // Obsługa przypadku, gdy rola nie istnieje
            }

            // Usuń stare role użytkownika
            var oldRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, oldRoles.ToArray());

            // Dodaj nową rolę użytkownikowi
            var result = await _userManager.AddToRoleAsync(user, role.Name);
            if (result.Succeeded)
            {
                // Obsługa przypadku, gdy zmiana roli powiedzie się
                return RedirectToAction("Users");
            }
            else
            {
                // Obsługa przypadku, gdy zmiana roli nie powiedzie się
                // Możesz obsłużyć błędy i przekazać je do widoku
                return View("Error");
            }
        }



    }
}
