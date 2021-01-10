using Microsoft.EntityFrameworkCore;
using POSSystemWithInventory.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSSystemWithInventory.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {
             
        }

        public DbSet<Brand> Brand { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<CompanyInformation> CompanyInformation { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<InvoiceDetails> InvoiceDetails { get; set; }
        public DbSet<Unit> Unit { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<PurchaseProduct> PurchaseProduct { get; set; }
        public DbSet<PurchaseProductDetail> PurchaseProductDetail { get; set; }
        public DbSet<Stock> Stock { get; set; }
        public DbSet<StockDetails> StockDetail { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<VatAndTax> VatAndTax { get; set; }
    }
}
