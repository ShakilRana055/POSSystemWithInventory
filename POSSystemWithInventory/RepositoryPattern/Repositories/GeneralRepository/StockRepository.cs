using POSSystemWithInventory.Data;
using POSSystemWithInventory.EntityModel;
using POSSystemWithInventory.RepositoryPattern.Interfaces.GeneralInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSSystemWithInventory.RepositoryPattern.Repositories.GeneralRepository
{
    public class StockRepository:BaseRepository<Stock>, IStockRepository
    {
        private readonly AppDbContext context;

        public StockRepository(AppDbContext appDb):base(appDb)
        {
            context = appDb;
        }

        public Stock GetLastOrDefault()
        {
            var result = context.Stock.OrderByDescending( x => x.Id).Take(1).FirstOrDefault();
            return result;
        }
    }
}
