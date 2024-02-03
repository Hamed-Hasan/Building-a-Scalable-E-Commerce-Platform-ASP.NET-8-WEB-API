using Microsoft.EntityFrameworkCore;

namespace EsapApi.Models
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Set the precision for the Price property of the Product entity
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2); // You can adjust these numbers as needed
        }
    }
}
