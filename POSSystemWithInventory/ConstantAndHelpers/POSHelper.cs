using Microsoft.AspNetCore.Mvc.Rendering;
using POSSystemWithInventory.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSSystemWithInventory.ConstantAndHelpers
{
    public static class POSHelper
    {
        public static List<SelectListItem> Brand(List<Brand>brands)
        {
            List<SelectListItem> brandsSelectList = new List<SelectListItem>();
            brandsSelectList.AddRange(brands.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }));
            return brandsSelectList;
        }
        public static List<SelectListItem> Category(List<Category> categories)
        {
            List<SelectListItem> categorySelectList = new List<SelectListItem>();
            categorySelectList.AddRange(categories.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }));
            return categorySelectList;
        }
        public static List<SelectListItem> Unit(List<Unit> units)
        {
            List<SelectListItem> unitSelectList = new List<SelectListItem>();
            unitSelectList.AddRange(units.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.ShortForm }));
            return unitSelectList;
        }
        public static List<SelectListItem> Supplier(List<Supplier> supplier)
        {
            List<SelectListItem> supplierSelectList = new List<SelectListItem>();
            supplierSelectList.AddRange(supplier.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = (x.Name +"|" + x.Phone) }));
            return supplierSelectList;
        }
        public static List<SelectListItem> Customer(List<Customer> customer)
        {
            List<SelectListItem> customerSelectList = new List<SelectListItem>();
            customerSelectList.AddRange(customer.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = (x.Name +"|" + x.Phone) }));
            return customerSelectList;
        }
        public static List<SelectListItem> Product(List<Product> products)
        {
            List<SelectListItem> productSelectList = new List<SelectListItem>();
            productSelectList.AddRange(products.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }));
            return productSelectList;
        }
        public static List<SelectListItem> VatAndTax(List<VatAndTax> vatAndTaxes)
        {
            List<SelectListItem> vatAndTaxSelectList = new List<SelectListItem>();
            vatAndTaxSelectList.AddRange(vatAndTaxes.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name + "|" + x.Amount }));
            return vatAndTaxSelectList;
        }
    }
}
