using System.ComponentModel.DataAnnotations;

namespace KutseApp_Viblyy.Models
{
    public class Holiday
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string Address { get; set; }

        public int ThinkingCount { get; set; } = 0; // <-- Новое поле



        // Навигационное свойство
        public ICollection<GuestResponse> Responses { get; set; }
    }
}
