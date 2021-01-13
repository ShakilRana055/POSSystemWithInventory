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
    }
}
