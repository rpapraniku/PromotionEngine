using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class InMemoryDbContext : DbContext
    {
        public InMemoryDbContext(DbContextOptions<InMemoryDbContext> options) : base(options)
        { }

        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
