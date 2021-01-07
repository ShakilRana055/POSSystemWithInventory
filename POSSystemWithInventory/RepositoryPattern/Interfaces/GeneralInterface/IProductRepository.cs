using POSSystemWithInventory.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSSystemWithInventory.RepositoryPattern.Interfaces.GeneralInterface
{
    public interface IProductRepository: IBaseRepository<Product>
    {
        public List<Product> GetAllWithRelatedData();
        public Product GetAllWithRelatedData(int id);
    }
}
