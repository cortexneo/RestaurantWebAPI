using Microsoft.EntityFrameworkCore;
using RestaurantWebAPI.Models;

namespace RestaurantWebAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt){}

        public DbSet<MenuItem> MenuItems { get; set; }

        public DbSet<Reservation> Reservations { get; set; }
    }
}
