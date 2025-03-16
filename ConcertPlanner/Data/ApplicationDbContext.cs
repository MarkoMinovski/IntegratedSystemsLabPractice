using ConcertPlanner.Areas.Identity;
using ConcertPlanner.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ConcertPlanner.Data
{
    public class ApplicationDbContext : IdentityDbContext<LabOneUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Concert> Concerts { get; set; } 
        public virtual DbSet<Ticket> Tickets { get; set; }
    }
}
