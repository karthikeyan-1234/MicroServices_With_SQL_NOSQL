using ERPModels;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace InventoryAPI.Infrastructure.Contexts
{
    public interface IInventoryDbContext
    {
        DbSet<Inventory>? Inventories { get; set; }
        Task<int> SaveChangesAsync(CancellationToken token = default);
        EntityEntry Entry(object entity);
        DbSet<T> Set<T>() where T : class;
    }
}