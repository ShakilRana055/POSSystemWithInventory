using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using POSSystemWithInventory.ConstantAndHelpers;
using POSSystemWithInventory.EntityModel;
using POSSystemWithInventory.Models;
using POSSystemWithInventory.RepositoryPattern.Interfaces.IUnitOfWork;

namespace POSSystemWithInventory.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork context;
        private readonly IImageProcessing image;

        public ProductController(IUnitOfWork unitOfWork, IImageProcessing imageProcessing)
        {
            context = unitOfWork;
            image = imageProcessing;
        }

        #region Product CRUD Operation
        public IActionResult Index()
        {
            var brand = context.Brand.GetAll().ToList();
            var category = context.Category.GetAll().ToList();
            var unit = context.Unit.GetAll().ToList();

            ProductVM productVM = new ProductVM();
            productVM.BrandItem = POSHelper.Brand(brand);
            productVM.CategoryItem = POSHelper.Category(category);
            productVM.UnitItem = POSHelper.Unit(unit);

            return View(productVM);
        }
        [HttpPost]
        public IActionResult Index(ProductVM productVM)
        {
            try
            {
                Product product = new Product()
                {
                    Name = productVM.Name,
                    Description = productVM.Description,
                    CategoryId = productVM.CategoryId,
                    BrandId = productVM.BrandId,
                    UnitId = productVM.UnitId,
                    WarningQuantity = productVM.WarningQuantity,
                };
                if (productVM.Photo != null)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(productVM.Photo.ContentDisposition).FileName.Trim('"').Replace(" ", string.Empty);
                    List<string> separate = fileName.Split(".").ToList();
                    fileName = separate[0] + DateTime.Now.ToString("dddd_dd_MMMM_yyyy_HH_mm_ss") + "." + separate[1];
                    string path = image.GetImagePath(fileName, "Product");
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        productVM.Photo.CopyTo(stream);
                    }
                    product.PhotoUrl = image.GetImagePathForDb(path);
                }
                context.Product.Add(product);
                context.Save();
                return Json(true);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
                throw;
            }
        }
        
        public IActionResult ProductEditViewUpdate(ProductVM productVM)
        {
            try
            {
                var products = context.Product.Find(x => x.Id == productVM.Id).FirstOrDefault();
                if (products != null)
                {
                    products.Name = productVM.Name;
                    products.Description = productVM.Description;
                    products.CategoryId = productVM.CategoryId;
                    products.BrandId = productVM.BrandId;
                    products.UnitId = productVM.UnitId;
                    products.WarningQuantity = productVM.WarningQuantity;
                    if (productVM.Photo != null)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(productVM.Photo.ContentDisposition).FileName.Trim('"').Replace(" ", string.Empty);
                        List<string> separate = fileName.Split(".").ToList();
                        fileName = separate[0] + DateTime.Now.ToString("dddd_dd_MMMM_yyyy_HH_mm_ss") + "." + separate[1];
                        string path = image.GetImagePath(fileName, "Product");
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            productVM.Photo.CopyTo(stream);
                        }
                        products.PhotoUrl = image.GetImagePathForDb(path);
                    }
                    context.Save();
                    return RedirectToAction("ProductListView");
                }
                else
                    return View(productVM);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
                throw;
            }
        }
        public IActionResult ProductList()
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

            var productList = context.Product.GetAllWithRelatedData();

            #region Filtering table data
            // searching 
            if (searchValue != null)
            {
                try
                {
                    var filteredProductList = productList.Where(
                        x => x.Name.ToLower().Contains(searchValue) ||
                        x.Description.ToLower().Contains(searchValue)||
                        x.Brand.Name.ToLower().Contains(searchValue) ||
                        x.Category.Name.ToLower().Contains(searchValue) ||
                        x.Unit.Name.ToLower().Contains(searchValue) ||
                        x.Unit.ShortForm.ToLower().Contains(searchValue)
                        ).ToList();
                    productList = filteredProductList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            #endregion

            var lists = productList.OrderByDescending(x => x.Id).ToList();

            //total number of rows count     
            recordsTotal = lists.Count();

            //Paging     
            var data = lists.Skip(skip).Take(pageSize).ToList();

            //Returning Json Data    
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
        }
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                var product = context.Product.Find(x => x.Id == id).FirstOrDefault();
                if (product != null)
                {
                    context.Product.Remove(product);
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
        public IActionResult ProductListView()
        {
            var brand = context.Brand.GetAll().ToList();
            var category = context.Category.GetAll().ToList();
            var unit = context.Unit.GetAll().ToList();

            ProductVM productVM = new ProductVM();
            productVM.BrandItem = POSHelper.Brand(brand);
            productVM.CategoryItem = POSHelper.Category(category);
            productVM.UnitItem = POSHelper.Unit(unit);

            return View(productVM);
        }
        public IActionResult ProductEditView(int search)
        {
            try
            {
                var products = context.Product.GetAllWithRelatedData(search);

                var brand = context.Brand.GetAll().ToList();
                var category = context.Category.GetAll().ToList();
                var unit = context.Unit.GetAll().ToList();

                if (products != null)
                {
                    ProductVM productVM = new ProductVM()
                    {
                        Id = search,
                        Name = products.Name,
                        WarningQuantity = products.WarningQuantity,
                        PhotoUrl = products.PhotoUrl,
                        Description = products.Description,
                        BrandId = products.BrandId,
                        CategoryId = products.CategoryId,
                        UnitId = products.UnitId,
                        BrandItem = POSHelper.Brand(brand),
                        CategoryItem = POSHelper.Category(category),
                        UnitItem = POSHelper.Unit(unit),
                    };
                    return View(productVM);
                }
                else
                {
                    ProductVM productVM = new ProductVM()
                    {
                        BrandItem = POSHelper.Brand(brand),
                        CategoryItem = POSHelper.Category(category),
                        UnitItem = POSHelper.Unit(unit),
                    };
                    return View(productVM);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        public IActionResult ProductInformation(int search)
        {
            try
            {
                var productInformation = context.Product.GetAllWithRelatedData(search);
                if (productInformation != null)
                {
                    ProductVM productVM = new ProductVM()
                    {
                        Name = productInformation.Name,
                        Description = productInformation.Description,
                        WarningQuantity = productInformation.WarningQuantity,
                        PhotoUrl = productInformation.PhotoUrl,
                        BrandName = productInformation.Brand != null ? productInformation.Brand.Name : "" ,
                        CategoryName = productInformation.Category != null ? productInformation.Category.Name : "" ,
                        UnitName = productInformation.Unit != null ? productInformation.Unit.Name : "" ,
                    };
                    return PartialView("_ProductInformation", productVM);
                }
                else
                {
                    ProductVM productVM = new ProductVM();
                    return PartialView("_ProductInformation", productVM);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
