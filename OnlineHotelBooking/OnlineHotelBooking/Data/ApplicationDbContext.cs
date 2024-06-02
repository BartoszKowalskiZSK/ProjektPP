using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineHotelBooking.Models;

namespace OnlineHotelBooking.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Rent> Rents { get; set; }
        public DbSet<Room> Rooms { get; set; }
    }
}
