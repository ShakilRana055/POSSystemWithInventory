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
        public Dictionary<string, decimal> PurchaseSummary(string startDate, string endDate)
        {
            DateTime newStartDate = Convert.ToDateTime(startDate);
            DateTime newEndDate = Convert.ToDateTime(endDate).AddDays(1);
            var grandTotal = context.PurchaseProduct.ToList()
                .Where(item => Convert.ToDateTime(item.CreatedDate) >= newStartDate && Convert.ToDateTime(item.CreatedDate) <= newEndDate)
                .Sum(item => item.GrandTotal);
            var dues = context.PurchaseProduct.ToList()
                .Where(item => Convert.ToDateTime(item.CreatedDate) >= newStartDate && Convert.ToDateTime(item.CreatedDate) <= newEndDate)
                .Sum(item => item.Dues);
            Dictionary<string, decimal> summary = new Dictionary<string, decimal>();
            summary.Add("purchaseTotal", grandTotal);
            summary.Add("purchaseDues", dues);
            return summary;
        }

        public decimal TodaysPurchase()
        {
            var response = context.PurchaseProduct.ToList()
                .Where(item => Convert.ToDateTime(item.CreatedDate).ToShortDateString() == DateTime.Now.ToShortDateString())
                .Sum(item => item.GrandTotal);
            return response;
        }
        public decimal TodaysPurchase(string currentDate)
        {
            var response = context.PurchaseProduct.ToList()
                .Where(item => Convert.ToDateTime(item.CreatedDate).ToShortDateString() == currentDate)
                .Sum(item => item.GrandTotal);
            return response;
        }
        public IEnumerable<PurchaseProduct> GetRelatedDataWithDate(DateTime startDate, DateTime endDate)
        {
            endDate = endDate.AddDays(1);
            var response = context.PurchaseProduct.Include(item => item.Supplier).ToList()
                            .Where(x => Convert.ToDateTime(x.CreatedDate) >= startDate && Convert.ToDateTime(x.CreatedDate) <= endDate)
                            .ToList();
            return response;
        }
        public IEnumerable<PurchaseProduct> GetDuesBySupplier(int supplierId)
        {
            var response = context.PurchaseProduct.ToList()
                           .Where(item => item.SupplierId == supplierId && item.Dues > 0)
                           .ToList();
            return response;
        }
    }
}
