using System.ComponentModel.DataAnnotations;

namespace KutseApp_Viblyy.Models
{
    public class GuestResponse
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }

        [Required]
        public string Response { get; set; } // "Приду" или "Еще подумаю"

        // Foreign Key
        public int HolidayId { get; set; }
        public Holiday Holiday { get; set; }
    }

}
