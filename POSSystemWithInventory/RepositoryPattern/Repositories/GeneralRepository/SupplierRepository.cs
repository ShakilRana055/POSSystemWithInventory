using Microsoft.EntityFrameworkCore;
using POSSystemWithInventory.Data;
using POSSystemWithInventory.EntityModel;
using POSSystemWithInventory.RepositoryPattern.Interfaces.GeneralInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSSystemWithInventory.RepositoryPattern.Repositories.GeneralRepository
{
    public class SupplierRepository:BaseRepository<Supplier>, ISupplierRepository
    {
        private readonly AppDbContext context;

        public SupplierRepository(AppDbContext appDb):base(appDb)
        {
            context = appDb;
        }
        public List<PurchaseProduct> SupplierReport()
        {
            var response = context.PurchaseProduct
                            .Include(x => x.Supplier)
                            .ToList()
                            .GroupBy(x => x.SupplierId);
            List<PurchaseProduct> supplier = new List<PurchaseProduct>();

            foreach (var item in response)
            {
                string name = "";
                decimal grandTotal = 0;
                decimal totalDues = 0;

                foreach (var iterate in item.ToList())
                {
                    name = iterate.Supplier.Name;
                    grandTotal += iterate.GrandTotal;
                    totalDues += iterate.Dues;
                }
                PurchaseProduct purchaseProduct = new PurchaseProduct()
                {
                    UpdatedBy = name,
                    GrandTotal = grandTotal,
                    Dues = totalDues,
                };
                supplier.Add(purchaseProduct);
            }
            return supplier;
        }
    }
}
