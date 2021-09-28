using DataAccess.Entities;
using DataAccess.Helpers;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class InMemoryDbContext : DbContext
    {
        public InMemoryDbContext(DbContextOptions<InMemoryDbContext> options) : base(options)
        { }

        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var stringValueConverter = new StringListToStringValueConverter();

            modelBuilder
                .Entity<Promotion>()
                .Property(e => e.SKUs)
                .HasConversion(stringValueConverter);

            base.OnModelCreating(modelBuilder);
        }
    }
}
