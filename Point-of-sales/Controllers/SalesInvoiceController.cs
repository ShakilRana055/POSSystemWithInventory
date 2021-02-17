using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using POSSystemWithInventory.ConstantAndHelpers;
using POSSystemWithInventory.EntityModel;
using POSSystemWithInventory.Models;
using POSSystemWithInventory.RepositoryPattern.Interfaces.IUnitOfWork;

namespace POSSystemWithInventory.Controllers
{
    public class SalesInvoiceController : Controller
    {
        private readonly IUnitOfWork context;

        public SalesInvoiceController(IUnitOfWork unitOfWork)
        {
            context = unitOfWork;
        }
        public IActionResult Index()
        {
            var customerItems = context.Customer.GetAll().ToList();
            var productItems = context.Product.GetAll().ToList();
            var vatItems = context.VatAndTax.GetAll().ToList();
            SalesInvoiceVM salesInvoice = new SalesInvoiceVM()
            {
                CustomerItem = POSHelper.Customer(customerItems),
                ProductItem = POSHelper.Product(productItems),
                VatItem = POSHelper.VatAndTax(vatItems),
            };
            
            return View(salesInvoice);
        }
        public IActionResult SalesInvoiceList()
        {
            return View();
        }

        public IActionResult Top10Sale()
        {
            var getIalesInvoiceList = context.SalesInvoiceDetail.Top10Sales();
            List<SalesInvoiceDetailVM> salesInvoice = new List<SalesInvoiceDetailVM>();
            foreach (var item in getIalesInvoiceList)
            {
                salesInvoice.Add(new SalesInvoiceDetailVM()
                {
                    ProductName = item.Key.Product.Name,
                    Count = item.Value,
                    PhotoUrl = item.Key.Product.PhotoUrl,
                });
            }
            var salesInvoiceList = new SalesInvoiceDetailVM();
            salesInvoiceList.SalesInvoiceDetail = salesInvoice;
            return View(salesInvoiceList);
        }
        [HttpPost]
        public IActionResult Index(SalesInvoiceVM salesInvoiceVM)
        {
            try
            {
                #region Sales invoice
                var customer = context.Customer.Find(item => item.Id == salesInvoiceVM.CustomerId).FirstOrDefault();
                decimal grandTotal = salesInvoiceVM.GrandTotal;
                decimal paidAmount = salesInvoiceVM.PaidAmount;
                if (salesInvoiceVM.IsBonusPointTaken)
                {
                    grandTotal += customer.BonusPoint;
                    paidAmount += customer.BonusPoint;
                }
                SalesInvoice salesInvoice = new SalesInvoice()
                {
                    InvoiceNumber = salesInvoiceVM.InvoiceNumber,
                    CustomerId = salesInvoiceVM.CustomerId,
                    SubTotal = salesInvoiceVM.SubTotal,
                    Discount = salesInvoiceVM.Discount,
                    GrandTotal = grandTotal,
                    PaidAmount = paidAmount,
                    Dues = salesInvoiceVM.Dues,
                    VatAndTaxId = salesInvoiceVM.VatAndTaxId,
                    PaymentMode = salesInvoiceVM.PaymentMode,
                };
                context.SalesInvoice.Add(salesInvoice);
                context.Save();

                foreach (var item in salesInvoiceVM.SalesInvoiceDetails)
                {
                    SalesInvoiceDetail salesInvoiceDetail = new SalesInvoiceDetail()
                    {
                        InvoiceNumber = item.InvoiceNumber,
                        SalesInvoiceId = salesInvoice.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice,
                        Price  = item.Price,
                    };
                    DecrementIneventory(item.ProductId, item.Quantity);
                    context.SalesInvoiceDetail.Add(salesInvoiceDetail);
                    context.Save();
                }
                #endregion

                #region Bonus Point Section
                if (salesInvoiceVM.IsBonusPointTaken)
                {
                    if (customer != null)
                    {
                        customer.BonusPoint = 0;
                        context.Save();
                    }
                }
                decimal bonusPoint = (salesInvoice.SubTotal / 100);
                if(customer != null && customer.Name != "Walk in Customer")
                {
                    customer.BonusPoint = customer.BonusPoint + bonusPoint;
                    context.Save();
                }
                #endregion

                return Json(true);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
                throw ex;
            }
        }

        #region Actions and Helpers

        private void DecrementIneventory(int? productId, decimal quantity)
        {
            var inventory = context.Inventory.Find(item => item.ProductId == productId).FirstOrDefault();
            if(inventory != null)
            {
                inventory.AvailableQuantity -= quantity;
                inventory.UpdatedDate = DateTime.Now.ToShortDateString();
                context.Save();
            }
        }
        private string InvoiceNumber()
        {
            var lastSalesInvoice = context.SalesInvoice.GetLastOrDefault();
            string invoiceNumber = AutoGeneratedNumber.GetInvoiceNumber(lastSalesInvoice == null ? "1": lastSalesInvoice.InvoiceNumber);
            return invoiceNumber;
        }
        public IActionResult GetInvoiceNumber()
        {
            string invoiceNumber = InvoiceNumber();
            return Json(invoiceNumber);
        }
        public IActionResult GetInventory()
        {
            var inventory = context.Inventory.GetAllWithRelatedData().ToList();
            return Json(inventory);
        }
        public IActionResult GetSalesInvoiceList()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDir = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault().ToLower();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            var salesInvoiceList = context.SalesInvoice.GetAllWithRelatedData();

            #region Filtering table data
            // searching 
            if (searchValue != null)
            {
                try
                {
                    var filteredProductList = salesInvoiceList.Where(
                        x => x.InvoiceNumber.ToLower().Contains(searchValue) ||
                        x.CreatedDate.ToLower().Contains(searchValue) ||
                        x.Customer.Name.ToLower().Contains(searchValue)
                        ).ToList();
                    salesInvoiceList = filteredProductList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            #endregion

            var lists = salesInvoiceList.OrderByDescending(x => x.Id).ToList();

            //total number of rows count     
            recordsTotal = lists.Count();

            //Paging     
            var data = lists.Skip(skip).Take(pageSize).ToList();

            //Returning Json Data    
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
        }
        public IActionResult SalesInvoiceInformation(string search)
        {
            string invoiceNumber = search;
            var salesInvoice = context.SalesInvoice.GetAllWithRelatedData(invoiceNumber);
            salesInvoice.SalesInvoiceDetails = context.SalesInvoiceDetail.GetAllWithRelatedData(invoiceNumber).ToList();
            salesInvoice.UpdatedBy = POSHelper.AmountInWords(salesInvoice.GrandTotal);
            return PartialView("_SalesInvoiceInformation", salesInvoice);
        }
        #endregion

        #region Accounts Receivable
        public IActionResult AccountReceivable()
        {
            var customer = context.Customer.GetAll().ToList();
            customer = customer.Where(item => item.Name != "Walk in Customer").ToList();
            SalesInvoiceVM salesInvoiceVM = new SalesInvoiceVM()
            {
                CustomerItem = POSHelper.Customer(customer),
            };
            return View(salesInvoiceVM);
        }
        [HttpPost]
        public IActionResult AccountReceivable(SalesInvoiceVM salesInvoiceVM)
        {
            try
            {
                foreach (var item in salesInvoiceVM.SalesInvoiceDetails)
                {
                    var salesInvoice = context.SalesInvoice.Find(x => x.InvoiceNumber == item.InvoiceNumber).FirstOrDefault();
                    if (salesInvoice != null)
                    {
                        salesInvoice.PaidAmount = salesInvoice.GrandTotal;
                        salesInvoice.Dues = 0;
                        context.Save();
                    }
                }
                return Json(true);
            }
            catch (Exception ex)
            {
                return Json(false);
                throw ex;
            }
        }
        public IActionResult GetDuesByCustomer(int search)
        {
            var dues = context.SalesInvoice.GetDuesByCustomer(search);
            return Json(dues);
        }

        #endregion
    }
}
