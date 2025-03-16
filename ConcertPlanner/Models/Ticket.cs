using ConcertPlanner.Areas.Identity;
using System.ComponentModel.DataAnnotations;

namespace ConcertPlanner.Models
{
    public class Ticket
    {
        [Key]
        public Guid Guid { get; set; }
        [Required]
        public int NumberOfPeople { get; set; }
        [Required]
        public Guid ConcertGuid { get; set; }
        public Concert? Concert { get; set; }
        public LabOneUser? Purchaser { get; set; }
        [Required]
        public string? PurchaserID { get; set; }
    }
}
