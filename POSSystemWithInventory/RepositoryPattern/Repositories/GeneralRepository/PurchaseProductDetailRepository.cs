using POSSystemWithInventory.Data;
using POSSystemWithInventory.EntityModel;
using POSSystemWithInventory.RepositoryPattern.Interfaces.GeneralInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSSystemWithInventory.RepositoryPattern.Repositories.GeneralRepository
{
    public class PurchaseProductDetailRepository: BaseRepository<PurchaseProductDetail>, IPurchaseProductDetailRepository
    {
        private readonly AppDbContext context;

        public PurchaseProductDetailRepository(AppDbContext appDbContex):base(appDbContex)
        {
            context = appDbContex;
        }
    }
}