using Microsoft.AspNetCore.Mvc.Rendering;
using POSSystemWithInventory.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSSystemWithInventory.ConstantAndHelpers
{
    public static class POSHelper
    {
        public static List<SelectListItem> Brand(List<Brand>brands)
        {
            List<SelectListItem> brandsSelectList = new List<SelectListItem>();
            brandsSelectList.AddRange(brands.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }));
            return brandsSelectList;
        }
        public static List<SelectListItem> Category(List<Category> categories)
        {
            List<SelectListItem> categorySelectList = new List<SelectListItem>();
            categorySelectList.AddRange(categories.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }));
            return categorySelectList;
        }
        public static List<SelectListItem> Unit(List<Unit> units)
        {
            List<SelectListItem> unitSelectList = new List<SelectListItem>();
            //unitSelectList.Add(new SelectListItem{Value = "0",Text = "-----Select-----"}) ;
            unitSelectList.AddRange(units.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.ShortForm }));
            return unitSelectList;
        }
    }
}
