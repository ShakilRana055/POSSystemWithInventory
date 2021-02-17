using POSSystemWithInventory.Data;
using POSSystemWithInventory.EntityModel;
using POSSystemWithInventory.RepositoryPattern.Interfaces.GeneralInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSSystemWithInventory.RepositoryPattern.Repositories.GeneralRepository
{
    public class InvoiceDetailsRepository: BaseRepository<InvoiceDetails> , IInvoiceDetailsRepository
    {
        private readonly AppDbContext context;

        public InvoiceDetailsRepository(AppDbContext appDb): base(appDb)
        {
            context = appDb;
        }
    }
}
