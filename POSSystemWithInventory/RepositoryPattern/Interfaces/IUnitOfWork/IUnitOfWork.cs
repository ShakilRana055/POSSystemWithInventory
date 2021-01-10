using POSSystemWithInventory.RepositoryPattern.Interfaces.GeneralInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSSystemWithInventory.RepositoryPattern.Interfaces.IUnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        int Save();
        public IUserRepository User { get; }
        public IBrandRepository Brand { get; }
        public ICategoryRepository Category { get; }
        public ICompanyInformationRepository CompanyInformation { get; }
        public ICustomerRepository Customer { get; }
        public IInvoiceRepository Invoice { get; }
        public IInventoryRepository Inventory { get; }
        public IInvoiceDetailsRepository InvoiceDetails { get; }
        public IProductRepository Product { get; }
        public IPurchaseProductRepository PurchaseProduct { get; }
        public IPurchaseProductDetailRepository PurchaseProductDetail { get; }
        public IStockRepository Stock { get; }
        public IStockDetailsRepository StockDetails { get; }
        public ISupplierRepository Supplier { get; }
        public IUnitRepository Unit { get; }
        public IVatAndTaxRepository VatAndTax { get; }
    }
}
