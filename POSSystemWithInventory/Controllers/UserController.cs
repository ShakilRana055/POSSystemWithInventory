using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using POSSystemWithInventory.EntityModel;
using POSSystemWithInventory.Models;
using POSSystemWithInventory.RepositoryPattern.Interfaces.IUnitOfWork;

namespace POSSystemWithInventory.Controllers
{
    public class UserController : Controller
    {
        private readonly IUnitOfWork context;
        private readonly IImageProcessing image;

        public UserController(IUnitOfWork unitOfWork, IImageProcessing imageProcessing)
        {
            context = unitOfWork;
            image = imageProcessing;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Supplier()
        {
            SupplierVM supplier = new SupplierVM();
            return View(supplier);
        }
        [HttpPost]
        public IActionResult Supplier(SupplierVM supplierVM)
        {
            try
            {
                Supplier supplier = new Supplier()
                {
                    Name = supplierVM.Name,
                    Email = supplierVM.Email,
                    NID = supplierVM.NID,
                    Address = supplierVM.Address,
                    Phone = supplierVM.Phone,
                    CompanyName = supplierVM.CompanyName,
                    Designation = supplierVM.Designation,
                };

                if (supplierVM.Photo != null)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(supplierVM.Photo.ContentDisposition).FileName.Trim('"').Replace(" ", string.Empty);
                    List<string> separate = fileName.Split(".").ToList();
                    fileName = separate[0] + DateTime.Now.ToString("dddd_dd_MMMM_yyyy_HH_mm_ss") + "." + separate[1];
                    string path = image.GetImagePath(fileName, "Supplier");
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        supplierVM.Photo.CopyTo(stream);
                    }
                    supplier.PhotoUrl = image.GetImagePathForDb(path);
                }
                context.Supplier.Add(supplier);
                context.Save();
                return Json(true);
            }
            catch(Exception ex)
            {
                return Json(ex.Message);
            }
            
        }

        public IActionResult SupplierList()
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

            var supplierList = context.Supplier.GetAll().ToList();

            #region Filtering table data
            // searching 
            if (searchValue != null)
            {
                try
                {
                    var filteredSupplierList = supplierList.Where(
                        x => x.Name.ToLower().Contains(searchValue) || 
                        x.Email.ToLower().Contains(searchValue) || 
                        x.Address.ToLower().Contains(searchValue)||
                        x.CompanyName.ToLower().Contains(searchValue)||
                        x.Phone.ToLower().Contains(searchValue)||
                        x.Designation.ToLower().Contains(searchValue)).ToList();
                    supplierList = filteredSupplierList;
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }

            #endregion

            var lists = supplierList.OrderByDescending(x => x.Id).ToList();

            //total number of rows count     
            recordsTotal = lists.Count();

            //Paging     
            var data = lists.Skip(skip).Take(pageSize).ToList();

            //Returning Json Data    
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
        }
        [HttpPost]
        public IActionResult SupplierUpdate(SupplierVM supplierVM)
        {
            try
            {
                var supplier = context.Supplier.Find( x => x.Id == supplierVM.Id).FirstOrDefault();
                if(supplier != null)
                {
                    supplier.Name = supplierVM.Name;
                    supplier.Email = supplierVM.Email;
                    supplier.NID = supplierVM.NID;
                    supplier.Address = supplierVM.Address;
                    supplier.Phone = supplierVM.Phone;
                    supplier.CompanyName = supplierVM.CompanyName;
                    supplier.Designation = supplierVM.Designation;
                    
                    if (supplierVM.Photo != null)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(supplierVM.Photo.ContentDisposition).FileName.Trim('"').Replace(" ", string.Empty);
                        List<string> separate = fileName.Split(".").ToList();
                        fileName = separate[0] + DateTime.Now.ToString("dddd_dd_MMMM_yyyy_HH_mm_ss") + "." + separate[1];
                        string path = image.GetImagePath(fileName, "Supplier");
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            supplierVM.Photo.CopyTo(stream);
                        }
                        supplier.PhotoUrl = image.GetImagePathForDb(path);
                    }
                    context.Save();
                    return Json(true);
                }
                else{
                    return Json(false);
                }
            }
            catch(Exception ex)
            {
                return Json(ex.Message);
            }
        }

        public IActionResult DeleteSupplier(int id){
            try{
                var supplier = context.Supplier.Find( x => x.Id == id).FirstOrDefault();
                if(supplier != null){
                    context.Supplier.Remove(supplier);
                    context.Save();
                    return Json(true);
                }
                else{
                    return Json(false);
                }
            }catch(Exception ex){
                return Json(ex.Message);
            }
        }
    }
}
