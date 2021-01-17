using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using POSSystemWithInventory.Models;
using POSSystemWithInventory.RepositoryPattern.Interfaces.IUnitOfWork;

namespace POSSystemWithInventory.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IUnitOfWork context;

        public DashboardController(IUnitOfWork unitOfWork)
        {
            context = unitOfWork;
        }
        
        public IActionResult Index( )
        {
            return View();
        }
        public IActionResult TodaysInformation()
        {
            var todaysPurchase = context.PurchaseProduct.TodaysPurchase();
            var todaysSales = context.SalesInvoice.TodaysSales();
            return Json(new { todaysPurchase, todaysSales });
        }
        public IActionResult InformationForChart()
        {
            DateTime dateTime = DateTime.Now;
            DateTime currentMonth = new DateTime(dateTime.Year, dateTime.Month, 1);

            List<string> days = new List<string>();
            List<decimal> purchase = new List<decimal>();
            List<decimal> sales = new List<decimal>();

            for(int i = 1; i <= dateTime.Day; i++)
            {
                string date = currentMonth.ToShortDateString();
                decimal todaysPurchase = context.PurchaseProduct.TodaysPurchase(date);
                decimal todaysSales = context.SalesInvoice.TodaysSales(date);

                days.Add(currentMonth.ToString("dd-MM-yy"));
                purchase.Add(todaysPurchase);
                sales.Add(todaysSales);

                currentMonth = currentMonth.AddDays(1);
            }
            return Json(new { days, purchase, sales });
        }
    }
}
