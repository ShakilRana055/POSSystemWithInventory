using POSSystemWithInventory.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSSystemWithInventory.RepositoryPattern.Interfaces.GeneralInterface
{
    public interface ISupplierRepository:IBaseRepository<Supplier>
    {
        public List<PurchaseProduct> SupplierReport();
    }
}
