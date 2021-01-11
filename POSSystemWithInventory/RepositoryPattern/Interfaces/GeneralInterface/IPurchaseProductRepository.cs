using POSSystemWithInventory.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSSystemWithInventory.RepositoryPattern.Interfaces.GeneralInterface
{
    public interface IPurchaseProductRepository: IBaseRepository<PurchaseProduct>
    {
        public PurchaseProduct GetLastOrDefault();
        public IEnumerable<PurchaseProduct> GetAllWithRelatedData();
        public PurchaseProduct GetAllWithRelatedData(string invoiceNumber);
    }
}
