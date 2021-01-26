using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using POSSystemWithInventory.EntityModel;
using POSSystemWithInventory.Models;
using POSSystemWithInventory.RepositoryPattern.Interfaces.IUnitOfWork;

namespace POSSystemWithInventory.Controllers
{
    public class ReportController : Controller
    {
        private readonly IUnitOfWork context;

        public ReportController(IUnitOfWork unitOfWork)
        {
            context = unitOfWork;
        }
        #region Stock Report
        public IActionResult StockReport()
        {
            return View();
        }
        public IActionResult GetStockReport()
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

            var inventoryList = context.Inventory.GetAllWithRelatedData();

            #region Filtering table data
            // searching 
            if (searchValue != null)
            {
                try
                {
                    var filterBrandList = inventoryList.Where(
                        x => x.Product.Name.ToLower().Contains(searchValue)).ToList();
                    inventoryList = filterBrandList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            #endregion

            var lists = inventoryList.OrderByDescending(x => x.Id).ToList();

            //total number of rows count     
            recordsTotal = lists.Count();

            //Paging     
            var data = lists.Skip(skip).Take(pageSize).ToList();

            //Returning Json Data    
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
        }
        #endregion

        #region Supplier Report
        public IActionResult SupplierReport()
        {
            return View();
        }
        public IActionResult SupplierReportList()
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

            var supplierList = context.Supplier.SupplierReport();

            #region Filtering table data
            // searching 
            if (searchValue != null)
            {
                try
                {
                    var filterBrandList = supplierList.Where(
                        x => x.UpdatedBy.ToLower().Contains(searchValue)).ToList();
                    supplierList = filterBrandList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            #endregion

            var lists = supplierList.OrderBy(item => item.UpdatedBy).ToList();

            //total number of rows count     
            recordsTotal = lists.Count();

            //Paging     
            var data = lists.Skip(skip).Take(pageSize).ToList();

            //Returning Json Data    
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
        }
        #endregion

        #region Customer Report
        public IActionResult CustomerReport()
        {
            return View();
        }
        public IActionResult CustomerReportList()
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

            var customerList = context.Customer.CustomerReport();

            #region Filtering table data
            // searching 
            if (searchValue != null)
            {
                try
                {
                    var filterBrandList = customerList.Where(
                        x => x.UpdatedBy.ToLower().Contains(searchValue)).ToList();
                    customerList = filterBrandList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            #endregion

            var lists = customerList.OrderBy(item => item.UpdatedBy).ToList();

            //total number of rows count     
            recordsTotal = lists.Count();

            //Paging     
            var data = lists.Skip(skip).Take(pageSize).ToList();

            //Returning Json Data    
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
        }
        #endregion

        #region Income Statement
        public IActionResult RevenueAndLoss()
        {
            return View();
        }
        public IActionResult IncomeStatement(IncomeStatement incomeStatement)
        {
            var purchaseSummary = context.PurchaseProduct.PurchaseSummary(incomeStatement.StartDate, incomeStatement.EndDate);
            var salesInvoiceSummary = context.SalesInvoice.SalesInvoiceSummary(incomeStatement.StartDate, incomeStatement.EndDate);
            IncomeStatement statement = new IncomeStatement()
            {
                StartDate = incomeStatement.StartDate,
                EndDate = incomeStatement.EndDate,
                TotalPurchaseAmount = purchaseSummary["purchaseTotal"],
                AccountPayable = purchaseSummary["purchaseDues"],
                TotalSalesAmount = salesInvoiceSummary["salesGrandTotal"],
                AccountReceivable = salesInvoiceSummary["salesDuesAmount"],
            };
            statement.FinalStatement = (statement.TotalSalesAmount + statement.AccountReceivable) - (statement.TotalPurchaseAmount + statement.AccountPayable);
            return PartialView("_IncomeStatement", statement);
        }
        #endregion

        #region IncomeReport Sales and Purchase List

        [HttpPost]
        public IActionResult PurchaseListInRange()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDir = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault().ToLower();
            var startDate = Convert.ToDateTime(Request.Form["startDate"].FirstOrDefault());
            var endDate = Convert.ToDateTime(Request.Form["endDate"].FirstOrDefault());

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            var purchaseList = context.PurchaseProduct.GetRelatedDataWithDate(startDate, endDate);
            #region Filtering table data
            // searching 
            if (searchValue != null)
            {
                try
                {
                    var filterBrandList = purchaseList.Where(
                        x => x.Supplier.Name.ToLower().Contains(searchValue) ||
                        x.InvoiceNumber.ToLower().Contains(searchValue)||
                        x.CreatedDate.ToLower().Contains(searchValue)
                        ).ToList();
                    purchaseList = filterBrandList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            #endregion

            var lists = purchaseList.OrderByDescending(x => x.Id).ToList();

            //total number of rows count     
            recordsTotal = lists.Count();

            //Paging     
            var data = lists.Skip(skip).Take(pageSize).ToList();

            //Returning Json Data    
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
        }

        [HttpPost]
        public IActionResult SalesListInRange()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDir = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault().ToLower();
            var startDate = Convert.ToDateTime(Request.Form["startDate"].FirstOrDefault());
            var endDate = Convert.ToDateTime(Request.Form["endDate"].FirstOrDefault());

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            var salesList = context.SalesInvoice.GetRelatedDataWithDate(startDate, endDate);
            #region Filtering table data
            // searching 
            if (searchValue != null)
            {
                try
                {
                    var filterBrandList = salesList.Where(
                        x => x.Customer.Name.ToLower().Contains(searchValue) ||
                        x.InvoiceNumber.ToLower().Contains(searchValue) ||
                        x.CreatedDate.ToLower().Contains(searchValue)
                        ).ToList();
                    salesList = filterBrandList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            #endregion

            var lists = salesList.OrderByDescending(x => x.Id).ToList();

            //total number of rows count     
            recordsTotal = lists.Count();

            //Paging     
            var data = lists.Skip(skip).Take(pageSize).ToList();

            //Returning Json Data    
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
        }
        #endregion
    }
}
