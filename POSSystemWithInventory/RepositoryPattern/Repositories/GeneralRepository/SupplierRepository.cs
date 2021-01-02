﻿using POSSystemWithInventory.Data;
using POSSystemWithInventory.EntityModel;
using POSSystemWithInventory.RepositoryPattern.Interfaces.GeneralInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSSystemWithInventory.RepositoryPattern.Repositories.GeneralRepository
{
    public class SupplierRepository:BaseRepository<Supplier>, ISupplierRepository
    {
        private readonly AppDbContext context;

        public SupplierRepository(AppDbContext appDb):base(appDb)
        {
            context = appDb;
        }
    }
}
