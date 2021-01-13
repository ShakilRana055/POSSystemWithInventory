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
    public class SalesInvoiceRepository: BaseRepository<SalesInvoice>, ISalesInvoiceRepository
    {
        private readonly AppDbContext context;

        public SalesInvoiceRepository(AppDbContext appDbContext): base(appDbContext)
        {
            context = appDbContext;
        }
        public SalesInvoice GetLastOrDefault()
        {
            var result = context.SalesInvoice.OrderByDescending(x => x.Id).Take(1).FirstOrDefault();
            return result;
        }
        public IEnumerable<SalesInvoice> GetAllWithRelatedData()
        {
            var response = context.SalesInvoice
                .Include(x => x.Customer)
                .Include(x => x.VatAndTax)
                .ToList();
            return response;
        }
        public SalesInvoice GetAllWithRelatedData(string invoiceNumber)
        {
            var response = context.SalesInvoice
                .Include(x => x.Customer)
                .Include(x => x.VatAndTax)
                .Where(item => item.InvoiceNumber == invoiceNumber)
                .FirstOrDefault();
            return response;
        }
    }
}
