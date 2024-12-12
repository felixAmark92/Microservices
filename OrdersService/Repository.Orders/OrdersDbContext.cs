using Microsoft.EntityFrameworkCore;
using Repository.Orders.Entities;

namespace Repository.Orders;

public class OrdersDbContext(DbContextOptions<OrdersDbContext> options) : DbContext

{
    public DbSet<OrderEntity> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
