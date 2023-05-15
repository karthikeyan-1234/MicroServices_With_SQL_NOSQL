using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using ERPModels;

namespace PurchaseAPI.Infrastructure.Contexts
{
    public interface IPurchaseDBContext
    {
        DbSet<PurchaseDetail>? PurchaseDetails { get; set; }
        DbSet<Purchase>? Purchases { get; set; }
        Task<int> SaveChangesAsync(CancellationToken token = default);
        EntityEntry Entry(object entity);
        DbSet<T> Set<T>() where T : class;
    }
}