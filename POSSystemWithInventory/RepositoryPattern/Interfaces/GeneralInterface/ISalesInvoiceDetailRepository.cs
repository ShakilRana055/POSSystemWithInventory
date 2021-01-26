using POSSystemWithInventory.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSSystemWithInventory.RepositoryPattern.Interfaces.GeneralInterface
{
    public interface ISalesInvoiceDetailRepository: IBaseRepository<SalesInvoiceDetail>
    {
        public IEnumerable<SalesInvoiceDetail> GetAllWithRelatedData(string invoiceNumber);
        public Dictionary<SalesInvoiceDetail, decimal> Top10Sales();
    }
}
