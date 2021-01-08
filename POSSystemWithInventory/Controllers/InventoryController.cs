﻿using System;
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
    public class InventoryController : Controller
    {
        private readonly IUnitOfWork context;

        public InventoryController(IUnitOfWork unitOfWork)
        {
            context = unitOfWork;
        }

        #region Purchase Product CRUD
        public IActionResult Index()
        {
            var supplierList = context.Supplier.GetAll().ToList();
            var productList = context.Product.GetAll().ToList();
            var vatAndTax = context.VatAndTax.GetAll().ToList();

            PurchaseProductVM purchaseProduct = new PurchaseProductVM()
            {
                SupplierItem = POSHelper.Supplier(supplierList),
                ProductItem = POSHelper.Product(productList),
                VatItem = POSHelper.VatAndTax(vatAndTax),
            };
            return View(purchaseProduct);
        }
        [HttpPost]
        public IActionResult Index(PurchaseProductVM purchaseProduct)
        {
            try
            {
                Stock stock = new Stock() 
                { 
                    InvoiceNumber = purchaseProduct.InvoiceNumber,
                    SupplierId = purchaseProduct.SupplierId,
                    TotalAmount = purchaseProduct.TotalAmount,
                    InvoiceStatus = purchaseProduct.InvoiceStatus,

                };

                return Json(true);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
                throw ex;
            }
        }
        public IActionResult PurchaseListView()
        {
            return View();
        }
        #endregion

        #region Actions & Helpers 

        private string GetInvoiceNumber()
        {
            var stockLastEntry = context.Stock.GetLastOrDefault();
            string invoiceNumber = AutoGeneratedNumber.GetStockNumber( stockLastEntry == null ? "1" : stockLastEntry.InvoiceNumber );
            return invoiceNumber;
        }
        public IActionResult InvoiceNumber()
        {
            string invoiceNumber = GetInvoiceNumber();
            return Json(invoiceNumber);
        }

        #endregion
    }
}