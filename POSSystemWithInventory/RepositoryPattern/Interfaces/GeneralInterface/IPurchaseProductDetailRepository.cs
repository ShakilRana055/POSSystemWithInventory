using POSSystemWithInventory.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSSystemWithInventory.RepositoryPattern.Interfaces.GeneralInterface
{
    public interface IPurchaseProductDetailRepository: IBaseRepository<PurchaseProductDetail>
    {
        public IEnumerable<PurchaseProductDetail> GetAllWithRelatedData(string invoiceNumber);
    }
}
