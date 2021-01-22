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
    public class SalesInvoiceDetailRepository: BaseRepository<SalesInvoiceDetail>, ISalesInvoiceDetailRepository
    {
        private readonly AppDbContext context;

        public SalesInvoiceDetailRepository(AppDbContext appDbContext):base(appDbContext)
        {
            context = appDbContext;
        }
        public IEnumerable<SalesInvoiceDetail> GetAllWithRelatedData(string invoiceNumber)
        {
            var response = context.SalesInvoiceDetail
                           .Include(x => x.Product)
                           .ThenInclude( x => x.Unit)
                           .Where(item => item.InvoiceNumber == invoiceNumber)
                           .ToList();
            return response;
        }
        public Dictionary <SalesInvoiceDetail, int> Top10Sales()
        {
            Dictionary<SalesInvoiceDetail, int> result = new Dictionary<SalesInvoiceDetail, int>();

            var response = context.SalesInvoiceDetail.Include(item => item.Product)
                            .Select(item => item.ProductId)
                            .Distinct()
                            .ToList();
            Dictionary<int?, int> storage = new Dictionary<int?, int>();

            foreach (var item in response)
            {
                var counting = context.SalesInvoiceDetail
                                .Where(x => x.ProductId == item)
                                .ToList()
                                .Count();
                storage.Add(item, counting);
            }
            int length = storage.Count;
            var newStorage = storage.OrderByDescending(item => item.Value).Take(length >= 10 ? 10 : length);
            foreach (var item in newStorage)
            {
                int count = item.Value;
                int? productId = item.Key;
                var salesInvoiceDetails = context.SalesInvoiceDetail.Include(item => item.Product)
                                          .Where(item => item.ProductId == productId)
                                          .FirstOrDefault();
                result.Add(salesInvoiceDetails, count);
            }
            return result;
        }
    }
}
