using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using POSSystemWithInventory.Data;
using POSSystemWithInventory.EntityModel;
using POSSystemWithInventory.RepositoryPattern.Interfaces.GeneralInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSSystemWithInventory.RepositoryPattern.Repositories.GeneralRepository
{
    public class PurchaseProductRepository: BaseRepository<PurchaseProduct>, IPurchaseProductRepository
    {
        private readonly AppDbContext context;

        public PurchaseProductRepository( AppDbContext appDbContext):base(appDbContext)
        {
            context = appDbContext;
        }
        public PurchaseProduct GetLastOrDefault()
        {
            var result = context.PurchaseProduct.OrderByDescending( x => x.Id).Take(1).FirstOrDefault();
            return result;
        }
        public IEnumerable<PurchaseProduct> GetAllWithRelatedData()
        {
            var result = context.PurchaseProduct.Include(x => x.Supplier).OrderByDescending( x => x.Id).ToList();
            return result;
        }
        public PurchaseProduct GetAllWithRelatedData(string invoiceNumber)
        {
            var result = context.PurchaseProduct.Include(x => x.Supplier).Where(x => x.InvoiceNumber == invoiceNumber).FirstOrDefault();
            return result;
        }
    }
}
