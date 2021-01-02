using POSSystemWithInventory.Data;
using POSSystemWithInventory.EntityModel;
using POSSystemWithInventory.RepositoryPattern.Interfaces;
using POSSystemWithInventory.RepositoryPattern.Interfaces.GeneralInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSSystemWithInventory.RepositoryPattern.Repositories.GeneralRepository
{
    public class UserRepository: BaseRepository<User>, IUserRepository
    {
        protected readonly AppDbContext context;
        public UserRepository( AppDbContext dbContext):base(dbContext)
        {
            context = dbContext;
        }
    }
}
