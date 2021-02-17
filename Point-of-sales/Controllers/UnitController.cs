using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using POSSystemWithInventory.EntityModel;
using POSSystemWithInventory.RepositoryPattern.Interfaces.IUnitOfWork;

namespace POSSystemWithInventory.Controllers
{
    public class UnitController : Controller
    {
        private readonly IUnitOfWork context;

        public UnitController(IUnitOfWork unitOfWork)
        {
            context = unitOfWork;
        }

        #region Unit CRUD operation
        public IActionResult Index()
        {
            Unit unit = new Unit();
            return View(unit);
        }
        [HttpPost]
        public IActionResult Index(Unit unit)
        {
            try
            {
                context.Unit.Add(unit);
                context.Save();
                return Json(true);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
                throw;
            }
        }
        public IActionResult UnitUpdate(Unit unit)
        {
            try
            {
                var getUnit = context.Unit.Find(x => x.Id == unit.Id).FirstOrDefault();
                if (getUnit != null)
                {
                    getUnit.Name = unit.Name;
                    getUnit.ShortForm = unit.ShortForm;
                    getUnit.IsUpdated = true;
                    getUnit.UpdatedDate = DateTime.Now.ToShortDateString();
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
        public IActionResult DeleteUnit(int id)
        {
            try
            {
                var unit = context.Unit.Find(x => x.Id == id).FirstOrDefault();
                if (unit != null)
                {
                    context.Unit.Remove(unit);
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
        public IActionResult UnitList()
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

            var unitList = context.Unit.GetAll().ToList();

            #region Filtering table data
            // searching 
            if (searchValue != null)
            {
                try
                {
                    var filterUnitList = unitList.Where(
                        x => x.Name.ToLower().Contains(searchValue) ||
                        x.ShortForm.ToLower().Contains(searchValue)).ToList();
                    unitList = filterUnitList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            #endregion

            var lists = unitList.OrderByDescending(x => x.Id).ToList();

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
