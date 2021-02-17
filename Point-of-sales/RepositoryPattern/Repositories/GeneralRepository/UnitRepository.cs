using POSSystemWithInventory.Data;
using POSSystemWithInventory.EntityModel;
using POSSystemWithInventory.RepositoryPattern.Interfaces.GeneralInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSSystemWithInventory.RepositoryPattern.Repositories.GeneralRepository
{
    public class UnitRepository:BaseRepository<Unit>, IUnitRepository
    {
        private readonly AppDbContext context;

        public UnitRepository(AppDbContext appDb):base(appDb)
        {
            context = appDb;
        }
    }
}
