using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace POSSystemWithInventory.Models
{
    public class ProductVM
    {
        public ProductVM()
        {
            CategoryItem = new List<SelectListItem>();
            BrandItem = new List<SelectListItem>();
            UnitItem = new List<SelectListItem>();

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public IFormFile Photo { get; set; }
        public string Description { get; set; }
        public string BrandName { get; set; }
        public string CategoryName { get; set; }
        public string UnitName { get; set; }
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }
        public int? UnitId { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal WarningQuantity { get; set; }
        public List<SelectListItem> CategoryItem { get; set; }
        public List<SelectListItem> BrandItem { get; set; }
        public List<SelectListItem> UnitItem { get; set; }
    }
}
