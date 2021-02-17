using POSSystemWithInventory.Data;
using POSSystemWithInventory.EntityModel;
using POSSystemWithInventory.RepositoryPattern.Interfaces.GeneralInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSSystemWithInventory.RepositoryPattern.Repositories.GeneralRepository
{
    public class CompanyInformationRepository:BaseRepository<CompanyInformation>, ICompanyInformationRepository
    {
        private readonly AppDbContext context;

        public CompanyInformationRepository(AppDbContext appDb):base(appDb)
        {
            context = appDb;
        }
    }
}
