using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using POSSystemWithInventory.EntityModel;

namespace POSSystemWithInventory.Controllers
{
    public class BrandController : Controller
    {
        public IActionResult Index()
        {
            Brand brand = new Brand();
            return View(brand);
        }

        [HttpPost]
        public IActionResult Index( Brand brand)
        {
            return View();
        }
    }
}
