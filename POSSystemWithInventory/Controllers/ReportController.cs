﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        #region Actions
        public IActionResult StockReport()
        {
            return View();
        }
        public IActionResult SupplierReport()
        {
            return View();
        }
        public IActionResult CustomerReport()
        {
            return View();
        }
        #endregion

        #region Action helper
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

    }
}