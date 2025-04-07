using System.ComponentModel.DataAnnotations;

namespace KutseApp_Viblyy.Models
{
    public class GuestResponseViewModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }

        [Required]
        public string Response { get; set; } // "Приду" или "Еще подумаю"

        public int HolidayId { get; set; }

        public string HolidayTitle { get; set; }
        public DateTime HolidayDate { get; set; }
        public string HolidayAddress { get; set; }
    }
}
