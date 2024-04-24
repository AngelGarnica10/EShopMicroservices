using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
    public class DiscountContext(DbContextOptions<DiscountContext> options) : DbContext(options)
    {
        public DbSet<Cupon> Cupons { get; set; } = default;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cupon>().HasData(
                new Cupon { Id = 1, ProductName = "IPhone X", Description = "IPhone Discount", Amount = 150 },
                new Cupon { Id = 2, ProductName = "Samsung 10", Description = "Samsung Discount", Amount = 100 }
                );
        }
    }
}
