using Domine.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    internal class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Categories> Category { get; set; }
        public DbSet<Dishes> Dish { get; set; }
        public DbSet<Orders> Order { get; set; }
        public DbSet<Sales> Sale { get; set; }
    }
}
