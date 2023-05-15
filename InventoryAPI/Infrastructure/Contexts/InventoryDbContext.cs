using ERPModels;

using Microsoft.EntityFrameworkCore;

namespace InventoryAPI.Infrastructure.Contexts
{
    public class InventoryDbContext : DbContext, IInventoryDbContext
    {
        public DbSet<Inventory>? Inventories { get; set; }

        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var Inventories = modelBuilder.Entity<Inventory>();

            Inventories.HasKey(i => i.id).IsClustered();

            base.OnModelCreating(modelBuilder);
        }
    }
}
