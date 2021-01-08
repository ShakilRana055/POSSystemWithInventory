using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace POSSystemWithInventory.Models
{
    public class PurchaseProductVM
    {
        public PurchaseProductVM()
        {
            PurchaseProductDetails = new List<PurchaseProductDetailVM>();
        }
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public int? SupplierId { get; set; }
        public List<SelectListItem> SupplierItem { get; set; }
        public List<SelectListItem> ProductItem { get; set; }
        public List<SelectListItem> VatItem { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal TotalAmount { get; set; }
        public bool InvoiceStatus { get; set; }
        public PurchaseProductDetailVM PurchaseProductDetail { get; set; }
        public List<PurchaseProductDetailVM> PurchaseProductDetails { get; set; }
    }
}
