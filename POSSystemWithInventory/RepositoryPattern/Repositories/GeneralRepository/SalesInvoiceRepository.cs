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
        public Dictionary<string, decimal> SalesInvoiceSummary(string startDate, string endDate)
        {
            DateTime newStartDate = Convert.ToDateTime(startDate);
            DateTime newEndDate = Convert.ToDateTime(endDate).AddDays(1);
            var grandTotal = context.SalesInvoice.ToList()
                .Where(item => Convert.ToDateTime(item.CreatedDate) >= newStartDate && Convert.ToDateTime(item.CreatedDate) <= newEndDate)
                .Sum(item => item.GrandTotal);
            var duesAmount = context.SalesInvoice.ToList()
                .Where(item => Convert.ToDateTime(item.CreatedDate) >= Convert.ToDateTime(startDate) && Convert.ToDateTime(item.CreatedDate) <= Convert.ToDateTime(endDate))
                .Sum(item => item.Dues);
            Dictionary<string, decimal> salesInvoiceSummary = new Dictionary<string, decimal>();
            salesInvoiceSummary.Add("salesGrandTotal", grandTotal);
            salesInvoiceSummary.Add("salesDuesAmount", duesAmount);
            return salesInvoiceSummary;
        }
        public decimal TodaysSales()
        {
            var response = context.SalesInvoice.ToList()
                           .Where(item => Convert.ToDateTime(item.CreatedDate).ToShortDateString() == DateTime.Now.ToShortDateString())
                           .Sum(item => item.GrandTotal);
            return response;
        }
        public decimal TodaysSales(string currentDate)
        {
            var grandTotal = context.SalesInvoice.ToList()
                           .Where(item => Convert.ToDateTime(item.CreatedDate).ToShortDateString() == currentDate)
                           .Sum(item => item.GrandTotal);
            return grandTotal;
        }

        public IEnumerable<SalesInvoice> GetRelatedDataWithDate(DateTime startDate, DateTime endDate)
        {
            endDate = endDate.AddDays(1);
            var response = context.SalesInvoice.Include(item => item.Customer).ToList()
                           .Where(item => Convert.ToDateTime(item.CreatedDate) >= startDate && Convert.ToDateTime(item.CreatedDate) <= endDate)
                           .ToList();
            return response;
        }
    }
}
