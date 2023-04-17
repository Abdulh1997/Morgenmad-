using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<WebApplication1.Model.Reservations> Reservations { get; set; } = default!;
        public DbSet<WebApplication1.Model.CheckedIn> CheckedIn { get; set; } = default!;
    }
}