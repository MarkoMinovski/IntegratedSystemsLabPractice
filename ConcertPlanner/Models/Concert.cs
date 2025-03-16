using System.ComponentModel.DataAnnotations;

namespace ConcertPlanner.Models
{
    public class Concert
    {
        [Key]
        public Guid Guid { get; set; }
        [Required]
        public string? ConcertName { get; set; }
        [Required]
        public string? ConcertPlace { get; set; }
        [Required]
        public DateOnly ConcertDate { get; set; }
        [Required]
        public decimal? ConcertPrice { get; set; }
    }

}
