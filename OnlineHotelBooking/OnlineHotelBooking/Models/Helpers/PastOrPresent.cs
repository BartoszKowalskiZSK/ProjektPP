using System.ComponentModel.DataAnnotations;

namespace OnlineHotelBooking.Models;

public class PastOrPresentDateAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        var date = (DateTime)value;
        return date <= DateTime.Today;
    }
}
