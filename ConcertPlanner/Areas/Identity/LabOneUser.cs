using Microsoft.AspNetCore.Identity;

namespace ConcertPlanner.Areas.Identity
{
    public class LabOneUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
    }
}
