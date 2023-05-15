using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;

using ERPModels;

namespace PurchaseAPI.Infrastructure.Contexts
{
    public class PurchaseDBContext : DbContext, IPurchaseDBContext
    {
        public DbSet<Purchase>? Purchases { get; set; }
        public DbSet<PurchaseDetail>? PurchaseDetails { get; set; }

        public PurchaseDBContext(DbContextOptions<PurchaseDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var Purchases = modelBuilder.Entity<Purchase>();
            var PurchaseDetails = modelBuilder.Entity<PurchaseDetail>();

            Purchases.HasKey(p => p.id).IsClustered();

            PurchaseDetails.HasKey(p => p.id).IsClustered();

            Purchases.HasMany(p => p.PurchaseDetails).WithOne(pd => pd.Purchase).HasForeignKey(pd => pd.purchase_id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
