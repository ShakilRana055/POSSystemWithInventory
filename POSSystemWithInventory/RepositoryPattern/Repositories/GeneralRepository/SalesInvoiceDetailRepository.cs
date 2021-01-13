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
    }
}
