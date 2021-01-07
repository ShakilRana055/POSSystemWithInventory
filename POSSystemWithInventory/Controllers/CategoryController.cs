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
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork context;
        private readonly IImageProcessing image;
        public CategoryController(IUnitOfWork unitOfWork, IImageProcessing imageProcessing)
        {
            context = unitOfWork;
            image = imageProcessing;
        }
        
        #region Category CRUD
        public IActionResult Index()
        {
            CategoryVM categoryVm = new CategoryVM();
            return View(categoryVm);
        }
        [HttpPost]
        public IActionResult Index(CategoryVM categoryVm)
        {
            try
            {
                Category category = new Category()
                {
                    Name = categoryVm.Name,
                    Code = categoryVm.Code,
                    Description = categoryVm.Description,
                };
                context.Category.Add(category);
                context.Save();
                return Json(true);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
                throw;
            }
        }
        public IActionResult CategoryUpdate(CategoryVM categoryVM)
        {
            try
            {
                var category = context.Category.Find(x => x.Id == categoryVM.Id).FirstOrDefault();
                if(category != null)
                {
                    category.Name = categoryVM.Name;
                    category.Code = categoryVM.Code;
                    category.Description = categoryVM.Description;
                    category.IsUpdated = true;
                    category.UpdatedDate = DateTime.Now.ToShortDateString();
                    context.Save();
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
            catch(Exception ex)
            {
                return Json(ex.Message);
                throw;
            }
        }
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                var category = context.Category.Find(x => x.Id == id).FirstOrDefault();
                if (category != null)
                {
                    context.Category.Remove(category);
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
        public IActionResult CategoryList()
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

            var categoryList = context.Category.GetAll().ToList();

            #region Filtering table data
            // searching 
            if (searchValue != null)
            {
                try
                {
                    var filterCategoryList = categoryList.Where(
                        x => x.Name.ToLower().Contains(searchValue) ||
                        x.Code.ToLower().Contains(searchValue) ||
                        x.Description.ToLower().Contains(searchValue)).ToList();
                    categoryList = filterCategoryList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            #endregion

            var lists = categoryList.OrderByDescending(x => x.Id).ToList();

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
