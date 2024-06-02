using OnlineHotelBooking.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace OnlineHotelBooking.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public string Number { get; set; }
        public RoomType RoomType { get; set; }
        public int Price { get; set; }

        public ICollection<Rent>? Rents { get; set; }
    }
}
