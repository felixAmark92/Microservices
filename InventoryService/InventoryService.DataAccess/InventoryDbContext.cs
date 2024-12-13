using InventoryService.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.DataAccess;

public class InventoryDbContext : DbContext
{
    public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options) { }
    public DbSet<InventoryEntity> Inventories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
       if (!optionsBuilder.IsConfigured)
       {
            optionsBuilder.UseInMemoryDatabase("PlaceholderDB"); // Use an in-memory provider
            return;
       }
       base.OnConfiguring(optionsBuilder);
    }
}