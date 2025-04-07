using KutseApp_Viblyy.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KutseApp_Viblyy.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<GuestResponse> GuestResponses { get; set; }
    }
}
