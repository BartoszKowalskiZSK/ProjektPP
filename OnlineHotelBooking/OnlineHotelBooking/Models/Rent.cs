using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineHotelBooking.Models
{
    public class Rent
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(UserId))]
        public string UserId { get; set; }
        public User? User { get; set; }

        [ForeignKey("Room")]
        public int? RoomId { get; set; }
        public Room? Room { get; set; }
        public bool Paid { get; set; }

        public DateTime From { get; set; }

        [Display(Name = "To")]
        [DataType(DataType.Date)]
        [FutureDate(ErrorMessage = "Data do musi być w przyszłości.")]
        [GreaterThan("From", ErrorMessage = "Data do musi być po dacie od.")]
        public DateTime To { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Cena musi być większa niż 0.")]
        public int Price { get; set; }
        public bool IsActive { get; set; }
    }

    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime date = (DateTime)value;
            return date > DateTime.Now;
        }
    }

    public class GreaterThanAttribute : ValidationAttribute
    {
        private string comparedPropertyName;

        public GreaterThanAttribute(string comparedPropertyName)
        {
            this.comparedPropertyName = comparedPropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var instance = validationContext.ObjectInstance;
            Type type = instance.GetType();
            var comparedValue = type.GetProperty(comparedPropertyName).GetValue(instance, null);

            if (value is DateTime dateValue && comparedValue is DateTime comparedDateValue && dateValue > comparedDateValue)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage);
        }
    }
}