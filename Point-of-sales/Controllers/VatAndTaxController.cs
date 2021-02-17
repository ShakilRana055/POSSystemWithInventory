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
    public class VatAndTaxController : Controller
    {
        private readonly IUnitOfWork context;
        private readonly IImageProcessing image;

        public VatAndTaxController(IUnitOfWork unitOfWork, IImageProcessing imageProcessing)
        {
            context = unitOfWork;
            image = imageProcessing;
        }
        #region Vat and Tax CRUD
        public IActionResult Index()
        {
            VatAndTaxVM vatAndTaxVM = new VatAndTaxVM();
            return View(vatAndTaxVM);
        }
        [HttpPost]
        public IActionResult Index(VatAndTaxVM vatAndTaxVM)
        {
            try
            {
                VatAndTax vatAndTax = new VatAndTax()
                {
                    Name = vatAndTaxVM.Name,
                    Amount = vatAndTaxVM.Amount,
                };
                context.VatAndTax.Add(vatAndTax);
                context.Save();
                return Json(true);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
                throw;
            }
        }
        public IActionResult VatAndTaxUpdate(VatAndTaxVM vatAndTaxVM)
        {
            try
            {
                var vatAndTax = context.VatAndTax.Find(x => x.Id == vatAndTaxVM.Id).FirstOrDefault();
                if (vatAndTax != null)
                {
                    vatAndTax.Name = vatAndTaxVM.Name;
                    vatAndTax.Amount = vatAndTaxVM.Amount;
                    vatAndTax.IsUpdated = true;
                    vatAndTax.UpdatedDate = DateTime.Now.ToShortDateString();
                    context.Save();
                    return Json(true);
                }
                else
                    return Json(false);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
                throw;
            }
        }
        public IActionResult DeleteVatAndTax(int id)
        {
            try
            {
                var vatAndTax = context.VatAndTax.Find(x => x.Id == id).FirstOrDefault();
                if (vatAndTax != null)
                {
                    context.VatAndTax.Remove(vatAndTax);
                    context.Save();
                    return Json(true);
                }
                else
                    return Json(false);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
                throw;
            }
        }
        public IActionResult VatAndTaxList()
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

            var vatAndTaxList = context.VatAndTax.GetAll().ToList();

            #region Filtering table data
            // searching 
            if (searchValue != null)
            {
                try
                {
                    var filteresVatAndTaxList = vatAndTaxList.Where(
                        x => x.Name.ToLower().Contains(searchValue) ||
                        Convert.ToString(x.Amount).Contains(searchValue)).ToList();
                    vatAndTaxList = filteresVatAndTaxList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            #endregion

            var lists = vatAndTaxList.OrderByDescending(x => x.Id).ToList();

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
