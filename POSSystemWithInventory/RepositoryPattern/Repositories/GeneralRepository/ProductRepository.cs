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
    public class ProductRepository: BaseRepository<Product>, IProductRepository
    {
        private readonly AppDbContext context;

        public ProductRepository(AppDbContext appDb):base(appDb)
        {
            context = appDb;
        }

        public List<Product> GetAllWithRelatedData()
        {
            var result = context.Product
                         .Include(x => x.Brand)
                         .Include(x => x.Category)
                         .Include(x => x.Unit)
                         .ToList();
            return result;
        }
        public Product GetAllWithRelatedData(int id)
        {
            var result = context.Product
                         .Include(x => x.Brand)
                         .Include(x => x.Category)
                         .Include(x => x.Unit)
                         .Where( x => x.Id == id)
                         .FirstOrDefault();
            return result;
        }
    }
}
