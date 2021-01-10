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

        [Column(TypeName = "decimal(16,2)")]
        public decimal Discount { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal Dues { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal GrandTotal { get; set; }
        public string InvoiceNumber { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal PaidAmount { get; set; }
        public string PaymentMode { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal SubTotal { get; set; }
        public int? SupplierId { get; set; }
        public List<SelectListItem> SupplierItem { get; set; }
        public List<SelectListItem> ProductItem { get; set; }
        public List<SelectListItem> VatItem { get; set; }
        public PurchaseProductDetailVM PurchaseProductDetail { get; set; }
        public List<PurchaseProductDetailVM> PurchaseProductDetails { get; set; }
    }
}
