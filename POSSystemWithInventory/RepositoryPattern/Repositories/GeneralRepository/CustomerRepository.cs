using POSSystemWithInventory.Data;
using POSSystemWithInventory.EntityModel;
using POSSystemWithInventory.RepositoryPattern.Interfaces.GeneralInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSSystemWithInventory.RepositoryPattern.Repositories.GeneralRepository
{
    public class CustomerRepository:BaseRepository<Customer>, ICustomerRepository
    {
        private readonly AppDbContext context;

        public CustomerRepository(AppDbContext appDb):base(appDb)
        {
            context = appDb;
        }
    }
}
