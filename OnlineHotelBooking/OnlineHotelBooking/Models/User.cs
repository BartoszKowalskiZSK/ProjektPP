using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineHotelBooking.Models
{
    public class User : IdentityUser
    {
        public string? Name { get; set; }

        public string? Surname { get; set; }
        public string? PESEL { set; get; }

        public ICollection<Rent>? Rents { get; set; }
    }

}
