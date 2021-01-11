using POSSystemWithInventory.Data;
using POSSystemWithInventory.RepositoryPattern.Interfaces.GeneralInterface;
using POSSystemWithInventory.RepositoryPattern.Interfaces.IUnitOfWork;
using POSSystemWithInventory.RepositoryPattern.Repositories.GeneralRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSSystemWithInventory.RepositoryPattern.Repositories.UnitOfWork
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly AppDbContext context;
        public UnitOfWork(AppDbContext databaseConnection)
        {
            context = databaseConnection;

            #region Creating Objects
            User = new UserRepository(context);
            Brand = new BrandRepository(context);
            Category = new CategoryRepository(context);
            CompanyInformation = new CompanyInformationRepository(context);
            Customer = new CustomerRepository(context);
            Invoice = new InvoiceRepository(context);
            Inventory = new InventoryRepository(context);
            InvoiceDetails = new InvoiceDetailsRepository(context);
            Product = new ProductRepository(context);
            SalesInvoice = new SalesInvoiceRepository(context);
            SalesInvoiceDetail = new SalesInvoiceDetailRepository(context);
            PurchaseProduct = new PurchaseProductRepository(context);
            PurchaseProductDetail = new PurchaseProductDetailRepository(context);
            Stock = new StockRepository(context);
            StockDetails = new StockDetailsRepository(context);
            Supplier = new SupplierRepository(context);
            Unit = new UnitRepository(context);
            VatAndTax = new VatAndTaxRepository(context);
            #endregion
        }

        #region SaveChanges
        public int Save()
        {
            return context.SaveChanges();
        }
        public void Dispose()
        {
            context.Dispose();
        }
        #endregion

        #region Implementation
        public IUserRepository User { get; private set; }
        public IBrandRepository Brand { get; private set; }
        public ICategoryRepository Category { get; private set; }
        public ICompanyInformationRepository CompanyInformation { get; private set; }
        public ICustomerRepository Customer { get; private set; }
        public IInvoiceRepository Invoice { get; private set; }
        public IInventoryRepository Inventory { get; private set; }
        public IInvoiceDetailsRepository InvoiceDetails { get; private set; }
        public IPurchaseProductRepository PurchaseProduct { get; private set; }
        public IPurchaseProductDetailRepository PurchaseProductDetail { get; private set; }
        public IProductRepository Product { get; private set; }
        public ISalesInvoiceRepository SalesInvoice { get; private set; }
        public ISalesInvoiceDetailRepository SalesInvoiceDetail { get; private set; }
        public IStockRepository Stock { get; private set; }
        public IStockDetailsRepository StockDetails { get; private set; }
        public ISupplierRepository Supplier { get; private set; }
        public IUnitRepository Unit { get; private set; }
        public IVatAndTaxRepository VatAndTax { get; private set; }
        #endregion
    }
}
