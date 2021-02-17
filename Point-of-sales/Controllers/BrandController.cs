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
    public class BrandController : Controller
    {
        private readonly IUnitOfWork context;
        private readonly IImageProcessing image;

        public BrandController(IUnitOfWork unitOfWork, IImageProcessing imageProcessing)
        {
            context = unitOfWork;
            image = imageProcessing;
        }
        public IActionResult Index()
        {
            BrandVM brand = new BrandVM();
            return View(brand);
        }
        [HttpPost]
        public IActionResult Index( BrandVM brandVM)
        {
            try
            {
                Brand brand = new Brand()
                {
                    Name = brandVM.Name,
                    Code = brandVM.Code,
                    Description = brandVM.Description,

                };
                if (brandVM.Logo != null)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(brandVM.Logo.ContentDisposition).FileName.Trim('"').Replace(" ", string.Empty);
                    List<string> separate = fileName.Split(".").ToList();
                    fileName = separate[0] + DateTime.Now.ToString("dddd_dd_MMMM_yyyy_HH_mm_ss") + "." + separate[1];
                    string path = image.GetImagePath(fileName, "Brand");
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        brandVM.Logo.CopyTo(stream);
                    }
                    brand.LogoUrl = image.GetImagePathForDb(path);
                }
                context.Brand.Add(brand);
                context.Save();
                return Json(true);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }
        public IActionResult BrandUpdate(BrandVM brandVM)
        {
            try
            {
                var brand = context.Brand.Find(x => x.Id == brandVM.Id).FirstOrDefault();
                if (brand != null)
                {
                    brand.Name = brandVM.Name;
                    brand.Code = brandVM.Code;
                    brand.Description = brandVM.Description;

                    if (brandVM.Logo != null)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(brandVM.Logo.ContentDisposition).FileName.Trim('"').Replace(" ", string.Empty);
                        List<string> separate = fileName.Split(".").ToList();
                        fileName = separate[0] + DateTime.Now.ToString("dddd_dd_MMMM_yyyy_HH_mm_ss") + "." + separate[1];
                        string path = image.GetImagePath(fileName, "Brand");
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            brandVM.Logo.CopyTo(stream);
                        }
                        brand.LogoUrl = image.GetImagePathForDb(path);
                    }
                    context.Save();
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }
        public IActionResult DeleteBrand(int id)
        {
            try
            {
                var brand = context.Brand.Find(x => x.Id == id).FirstOrDefault();
                if (brand != null)
                {
                    context.Brand.Remove(brand);
                    context.Save();
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }
        public IActionResult BrandList()
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

            var brandList = context.Brand.GetAll().ToList();

            #region Filtering table data
            // searching 
            if (searchValue != null)
            {
                try
                {
                    var filterBrandList = brandList.Where(
                        x => x.Name.ToLower().Contains(searchValue) ||
                        x.Code.ToLower().Contains(searchValue) ||
                        x.Description.ToLower().Contains(searchValue)).ToList();
                    brandList = filterBrandList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            #endregion

            var lists = brandList.OrderByDescending(x => x.Id).ToList();

            //total number of rows count     
            recordsTotal = lists.Count();

            //Paging     
            var data = lists.Skip(skip).Take(pageSize).ToList();

            //Returning Json Data    
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
        }
        public IActionResult GetDuplicate()
        {
            var result = context.Brand.GetAll().ToList();
            return Json(result);
        }
    }
}
