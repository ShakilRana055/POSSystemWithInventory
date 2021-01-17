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
    public class CustomerRepository:BaseRepository<Customer>, ICustomerRepository
    {
        private readonly AppDbContext context;

        public CustomerRepository(AppDbContext appDb):base(appDb)
        {
            context = appDb;
        }
        public List<SalesInvoice> CustomerReport()
        {
            var reponse = context.SalesInvoice.Include(x => x.Customer).ToList().GroupBy(x => x.CustomerId);
            List<SalesInvoice> customerReport = new List<SalesInvoice>();
            foreach (var item in reponse)
            {
                string customerName = "";
                decimal totalAmount = 0;
                decimal dueAmount = 0;
                foreach (var iterate in item.ToList())
                {
                    customerName = iterate.Customer.Name;
                    totalAmount += iterate.GrandTotal;
                    dueAmount += iterate.Dues;
                }
                SalesInvoice salesInvoice = new SalesInvoice()
                {
                    UpdatedBy = customerName,
                    GrandTotal = totalAmount,
                    Dues = dueAmount,
                };
                customerReport.Add(salesInvoice);
            }
            return customerReport;
        }
    }
}
