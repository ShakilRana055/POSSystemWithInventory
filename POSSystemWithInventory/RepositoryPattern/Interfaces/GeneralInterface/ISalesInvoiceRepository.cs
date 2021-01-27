using POSSystemWithInventory.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSSystemWithInventory.RepositoryPattern.Interfaces.GeneralInterface
{
    public interface ISalesInvoiceRepository: IBaseRepository<SalesInvoice>
    {
        public SalesInvoice GetLastOrDefault();
        public IEnumerable<SalesInvoice> GetAllWithRelatedData();
        public SalesInvoice GetAllWithRelatedData(string invoiceNumber);
        public Dictionary<string , decimal> SalesInvoiceSummary(string startDate, string endDate);
        public decimal TodaysSales();
        public decimal TodaysSales(string currentDate);
        public IEnumerable<SalesInvoice> GetRelatedDataWithDate(DateTime startDate, DateTime endDate);
        public IEnumerable<SalesInvoice> GetDuesByCustomer(int customerId);
    }
}
